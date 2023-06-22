using System.Security.Claims;
using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.ContactInfos;
using HospitalAPI.Models.Genders;
using HospitalAPI.Models.Patients;
using HospitalAPI.Services.Patients;
using HospitalAPI.Utils.Identity;
using HospitalAPI.Utils.Identity.Extensions;
using HospitalAPI.Utils.Roles;
using HospitalAPI.Utils.Roles.Attributes;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        private readonly IMapper _mapper;

        public PatientsController(
            IPatientsService patientsService,
            IMapper mapper
        )
        {
            _patientsService = patientsService;
            _mapper = mapper;
        }

        [RoleAuthorize(EUserRole.Administrator | EUserRole.Doctor)]
        [HttpGet("getPatients")]
        public async Task<ActionResult<PatientReadDto>> GetPatients()
        {
            var patients = await _patientsService.GetPatients();

            var patientReadDtos = patients.Select(MapToReadDto);

            return Ok(patientReadDtos);
        }

        [Authorize]
        [HttpGet("getPatient/{id}")]
        public async Task<ActionResult<PatientReadDto>> GetPatient(int id)
        {
            var userIdRole = User.GetClaimIntValue(ClaimType.IdRole);
            if (!userIdRole.HasValue)
                return Unauthorized("Not authorized user");
            var userRole = RolesExtension.ToUserRole(userIdRole.Value);
            if (userRole == EUserRole.Patient)
            {
                var idPatient = User.GetClaimIntValue(ClaimType.IdPatient);
                if (!idPatient.HasValue || idPatient.Value != id)
                    return Unauthorized();
            }


            var patient = await _patientsService.GetPatientById(id);
            if (patient == null)
                return NotFound();

            var patientReadDto = MapToReadDto(patient);

            return Ok(patientReadDto);
        }

        private PatientReadDto MapToReadDto(Patient patient)
        {
            return new PatientReadDto
            {
                IdPatient = patient.IdPatient,
                IdUser = patient.IdUser,
                IdMedicalCard = patient.MedicalCards.FirstOrDefault()!.IdMedicalCard,
                Gender = _mapper.Map<GenderReadDto>(patient.IdGenderNavigation),
                Firstname = patient.Firstname,
                Lastname = patient.Lastname,
                BirthDate = patient.BirthDate,
                ContactInfos = _mapper.Map<IEnumerable<ContactInfoReadDto>>(patient.IdContactInfos)
            };
        }

        [RoleAuthorize(EUserRole.Patient)]
        [HttpPut("changePatient/{id}")]
        public async Task<ActionResult> ChangePatient(int id, PatientUpdateDto patientUpdateDto)
        {
            var patient = await _patientsService.GetPatientById(id);
            if (patient == null)
                return NotFound();

            _mapper.Map(patientUpdateDto, patient);

            await _patientsService.UpdatePatient(patient);

            return NoContent();
        }

        [HttpGet("getContactInfos")]
        public async Task<ActionResult<ContactInfoReadDto>> GetContactInfos(int patientId)
        {
            var patient = await _patientsService.GetPatientById(patientId);
            if (patient == null)
                return NotFound();

            var contactInfos = await _patientsService.GetContactInfosByPatient(patient);

            var contactInfosReadDto = _mapper.Map<IEnumerable<ContactInfoReadDto>>(contactInfos);

            return Ok(contactInfosReadDto);
        }

        [RoleAuthorize(EUserRole.Patient)]
        [HttpPost("createContactInfo")]
        public async Task<ActionResult> CreateContactInfo(ContactInfoCreateDto contactInfoCreateDto)
        {
            //TODO it is needed to take logined patient first and than create a contactInfo for him
            var patientId = 1;
            var patient = await _patientsService.GetPatientById(patientId);

            var contactInfo = _mapper.Map<ContactInfo>(contactInfoCreateDto);
            contactInfo.IdPatients.Add(patient!);

            await _patientsService.CreateContactInfo(contactInfo);

            var contactInfoReadDto = _mapper.Map<ContactInfoReadDto>(contactInfo);

            return Created(string.Empty, contactInfoReadDto);
        }

        [RoleAuthorize(EUserRole.Patient)]
        [HttpDelete("removeContactInfo")]
        public async Task<ActionResult> RemoveContactInfo(int id)
        {
            var contactInfo = await _patientsService.GetContactInfoById(id);

            if (contactInfo == null)
                return NotFound();

            await _patientsService.DeleteContactInfo(contactInfo);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Patient)]
        [HttpGet("getMyInfo")]
        public async Task<ActionResult<PatientReadDto>> GetMyInfo()
        {
            var patientId = User.GetClaimIntValue(ClaimType.IdPatient);
            if (!patientId.HasValue)
                return Unauthorized();

            var patient = await _patientsService.GetPatientById(patientId.Value);
            if (patient == null)
                return NotFound();

            var patientReadDto = _mapper.Map<PatientReadDto>(patient);

            return Ok(patientReadDto);
        }
    }
}