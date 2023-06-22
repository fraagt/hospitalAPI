using System.Collections;
using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentTimes;
using HospitalAPI.Models.ContactInfos;
using HospitalAPI.Models.Doctors;
using HospitalAPI.Models.Genders;
using HospitalAPI.Models.Patients;
using HospitalAPI.Models.Services;
using HospitalAPI.Models.Shifts;
using HospitalAPI.Models.Specialities;
using HospitalAPI.Models.WorkHistories;
using HospitalAPI.Services.Doctors;
using HospitalAPI.Utils.Identity;
using HospitalAPI.Utils.Identity.Extensions;
using HospitalAPI.Utils.Roles.Attributes;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorsService;
        private readonly IMapper _mapper;

        public DoctorsController(
            IDoctorsService doctorsService,
            IMapper mapper)
        {
            _doctorsService = doctorsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("getDoctors")]
        public async Task<ActionResult<DoctorReadDto>> GetDoctors([FromQuery] GetDoctorsFilters filters)
        {
            var doctors = await _doctorsService.GetDoctors(filters);

            var doctorReadDtos = doctors.Select(MapToReadDto).ToList();

            return Ok(doctorReadDtos);
        }

        [AllowAnonymous]
        [HttpGet("getDoctor/{id}")]
        public async Task<ActionResult<DoctorReadDto>> GetDoctor(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var doctor = await _doctorsService.GetDoctorById(id);

            if (doctor == null)
                return NotFound();

            var doctorReadDto = MapToReadDto(doctor);

            return Ok(doctorReadDto);
        }

        private DoctorReadDto MapToReadDto(Doctor doctor)
        {
            return new DoctorReadDto
            {
                IdDoctor = doctor.IdDoctor,
                IdUser = doctor.IdUser,
                Gender = _mapper.Map<GenderReadDto>(doctor.IdGenderNavigation),
                Services = _mapper.Map<IEnumerable<ServiceReadDto>>(doctor.IdServices),
                ContactInfos = _mapper.Map<IEnumerable<ContactInfoReadDto>>(doctor.IdContactInfos),
                Specialities = _mapper.Map<IEnumerable<SpecialityReadDto>>(doctor.IdSpecialities),
                WorkHistories = _mapper.Map<IEnumerable<WorkHistoryReadDto>>(doctor.WorkHistories),
                AppointmentTimes = _mapper.Map<IEnumerable<AppointmentTimeReadDto>>(doctor.AppointmentTimes),
                Biography = doctor.Biography,
                BirthDate = doctor.BirthDate,
                Firstname = doctor.Firstname,
                Lastname = doctor.Lastname
            };
        }

        [RoleAuthorize(EUserRole.Administrator | EUserRole.Doctor)]
        [HttpPut("changeDoctor/{id}")]
        public async Task<ActionResult> ChangeDoctor(int id, DoctorUpdateDto doctorUpdateDto)
        {
            var doctor = await _doctorsService.GetDoctorById(id);

            if (doctor == null)
                return NotFound();

            _mapper.Map(doctorUpdateDto, doctor);

            await _doctorsService.UpdateDoctor(doctor);

            return NoContent();
        }
        [HttpGet("getWorkHistories")]
        public async Task<ActionResult<WorkHistoryReadDto>> GetWorkHistories()
        {
            var workHistories = await _doctorsService.GetWorkHistories();

            var workHistoriesReadDto = _mapper.Map<IEnumerable<WorkHistoryReadDto>>(workHistories);

            return Ok(workHistoriesReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createWorkHistory")]
        public async Task<ActionResult<WorkHistoryReadDto>> CreateWorkHistory(WorkHistoryCreateDto workHistoryCreateDto)
        {
            //TODO it is needed to take logined doctor first and than create a workHistory for him
            var doctorId = 1;
            var doctor = await _doctorsService.GetDoctorById(doctorId);

            var workHistory = _mapper.Map<WorkHistory>(workHistoryCreateDto);
            workHistory.IdDoctorNavigation = doctor!;
            await _doctorsService.CreateWorkHistory(workHistory);

            var workHistoryReadDto = _mapper.Map<WorkHistoryReadDto>(workHistory);
            return Created(string.Empty, workHistoryReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeWorkHistory")]
        public async Task<ActionResult> RemoveWorkHistory(int id)
        {
            var workHistory = await _doctorsService.GetWorkHistoryById(id);

            if (workHistory == null)
                return NotFound();

            await _doctorsService.DeleteWorkHistory(workHistory);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("getSpecialities")]
        public async Task<ActionResult<SpecialityReadDto>> GetSpecialities()
        {
            var specialities = await _doctorsService.GetSpecialities();

            var specialitiesReadDto = _mapper.Map<IEnumerable<SpecialityReadDto>>(specialities);

            return Ok(specialitiesReadDto);
        }

        [RoleAuthorize(EUserRole.Administrator)]
        [HttpPost("createSpeciality")]
        public async Task<ActionResult<SpecialityReadDto>> CreateSpeciality(SpecialityCreateDto specialityCreateDto)
        {
            var speciality = _mapper.Map<Speciality>(specialityCreateDto);

            await _doctorsService.CreateSpeciality(speciality);

            var specialityReadDto = _mapper.Map<SpecialityReadDto>(speciality);

            return Created(string.Empty, specialityReadDto);
        }

        [RoleAuthorize(EUserRole.Administrator)]
        [HttpDelete("removeSpeciality")]
        public async Task<ActionResult> RemoveSpeciality(int id)
        {
            var speciality = await _doctorsService.GetSpecialityById(id);

            if (speciality == null)
                return NotFound();

            await _doctorsService.DeleteSpeciality(speciality);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("addDoctorSpeciality")]
        public async Task<ActionResult> AddDoctorSpeciality(int doctorId, int specialityId)
        {
            var doctor = await _doctorsService.GetDoctorById(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found");

            await _doctorsService.LoadDoctorSpecialities(doctor);
            if (doctor.IdSpecialities.Any(s => s.IdSpeciality == specialityId))
                return BadRequest("Doctor already has this speciality");

            var speciality = await _doctorsService.GetSpecialityById(specialityId);
            if (speciality == null)
                return NotFound("Speciality not found");

            doctor.IdSpecialities.Add(speciality);
            await _doctorsService.UpdateDoctor(doctor);

            return Ok();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeDoctorSpeciality")]
        public async Task<ActionResult> RemoveDoctorSpeciality(int doctorId, int specialityId)
        {
            var doctor = await _doctorsService.GetDoctorById(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found");

            await _doctorsService.LoadDoctorSpecialities(doctor);
            var speciality = doctor.IdSpecialities.FirstOrDefault(s => s.IdSpeciality == specialityId);

            if (speciality == null)
                return NotFound("Doctor doesn't have this speciality");

            doctor.IdSpecialities.Remove(speciality);
            await _doctorsService.UpdateDoctor(doctor);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("getAppointmentTimes")]
        public async Task<ActionResult<AppointmentTimeReadDto>> GetAppointmentTimes(
            [FromQuery] AppointmentTimeFilters filters)
        {
            var appointmentTimes = await _doctorsService.GetAppointmentTimes(filters);

            var appointmentTimesReadDto = _mapper.Map<IEnumerable<AppointmentTimeReadDto>>(appointmentTimes);

            return Ok(appointmentTimesReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createAppointmentTime")]
        public async Task<ActionResult> CreateAppointmentTime(AppointmentTimeCreateDto appointmentTimeCreateDto)
        {
            //TODO it is needed to take logined doctor first and than create a workHistory for him
            var doctorId = 1;
            var doctor = await _doctorsService.GetDoctorById(doctorId);

            var appointmentTime = _mapper.Map<AppointmentTime>(appointmentTimeCreateDto);
            appointmentTime.IdDoctorNavigation = doctor!;

            await _doctorsService.CreateAppointmentTime(appointmentTime);

            var appointmentTimeReadDto = _mapper.Map<AppointmentTimeReadDto>(appointmentTime);

            return Created(string.Empty, appointmentTimeReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeAppointmentTime")]
        public async Task<ActionResult> RemoveAppointmentTime(int id)
        {
            var appointmentTime = await _doctorsService.GetAppointmentTimeById(id);

            if (appointmentTime == null)
                return NotFound();

            await _doctorsService.DeleteAppointmentTime(appointmentTime);

            return NoContent();
        }

        [HttpGet("getServices")]
        public async Task<ActionResult<ServiceReadDto>> GetServices()
        {
            var services = await _doctorsService.GetServices();

            var servicesReadDto = _mapper.Map<IEnumerable<ServiceReadDto>>(services);

            return Ok(servicesReadDto);
        }

        [RoleAuthorize(EUserRole.Administrator)]
        [HttpPost("createService")]
        public async Task<ActionResult> CreateService(ServiceCreateDto serviceCreateDto)
        {
            var service = _mapper.Map<Service>(serviceCreateDto);

            await _doctorsService.CreateService(service);

            var serviceReadDto = _mapper.Map<ServiceReadDto>(service);

            return Created(string.Empty, serviceReadDto);
        }

        [RoleAuthorize(EUserRole.Administrator)]
        [HttpDelete("removeService")]
        public async Task<ActionResult> RemoveService(int id)
        {
            var service = await _doctorsService.GetServiceById(id);

            if (service == null)
                return NotFound();

            await _doctorsService.DeleteService(service);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("addDoctorService")]
        public async Task<ActionResult> AddDoctorService(int doctorId, int serviceId)
        {
            var doctor = await _doctorsService.GetDoctorById(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found");

            await _doctorsService.LoadDoctorServices(doctor);
            if (doctor.IdServices.Any(s => s.IdService == serviceId))
                return BadRequest("Doctor already has this service");

            var service = await _doctorsService.GetServiceById(serviceId);
            if (service == null)
                return NotFound("Service not found");

            doctor.IdServices.Add(service);
            await _doctorsService.UpdateDoctor(doctor);

            return Ok();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeDoctorService")]
        public async Task<ActionResult> RemoveDoctorService(int doctorId, int serviceId)
        {
            var doctor = await _doctorsService.GetDoctorById(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found");

            await _doctorsService.LoadDoctorServices(doctor);
            var service = doctor.IdServices.FirstOrDefault(s => s.IdService == serviceId);

            if (service == null)
                return NotFound("Doctor doesn't have this speciality");

            doctor.IdServices.Remove(service);
            await _doctorsService.UpdateDoctor(doctor);

            return NoContent();
        }

        [HttpGet("getShifts")]
        public async Task<ActionResult<ShiftReadDto>> GetShifts()
        {
            var shifts = await _doctorsService.GetShifts();

            var shiftsReadDto = _mapper.Map<IEnumerable<ShiftReadDto>>(shifts);

            return Ok(shiftsReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createShift")]
        public async Task<ActionResult> CreateShift(ShiftCreateDto shiftCreateDto)
        {
            //TODO it is needed to take logined doctor first and than create a workHistory for him
            var doctorId = 1;

            var shift = _mapper.Map<Shift>(shiftCreateDto);
            shift.IdDoctor = doctorId;

            await _doctorsService.CreateShift(shift);

            var shiftReadDto = _mapper.Map<ShiftReadDto>(shift);

            return Created(string.Empty, shiftReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeShift")]
        public async Task<ActionResult> RemoveShift(int id)
        {
            var shift = await _doctorsService.GetShiftById(id);

            if (shift == null)
                return NotFound();

            await _doctorsService.DeleteShift(shift);

            return NoContent();
        }

        [HttpGet("getContactInfos")]
        public async Task<ActionResult<ContactInfoReadDto>> GetContactInfos(int doctorId)
        {
            var doctor = await _doctorsService.GetDoctorById(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found");

            var contactInfos = await _doctorsService.GetContactInfosByDoctor(doctor);

            var contactInfosReadDto = _mapper.Map<IEnumerable<ContactInfoReadDto>>(contactInfos);
            return Ok(contactInfosReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createContactInfo")]
        public async Task<ActionResult> CreateContactInfo(ContactInfoCreateDto contactInfoCreateDto)
        {
            //TODO it is needed to take logined doctor first and than create a workHistory for him
            var doctorId = 1;
            var doctor = await _doctorsService.GetDoctorById(doctorId);

            var contactInfo = _mapper.Map<ContactInfo>(contactInfoCreateDto);
            contactInfo.IdDoctors.Add(doctor!);

            await _doctorsService.CreateContactInfo(contactInfo);

            var contactInfoReadDto = _mapper.Map<ContactInfoReadDto>(contactInfo);

            return Created(string.Empty, contactInfoReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeContactInfo")]
        public async Task<ActionResult> RemoveContactInfo(int id)
        {
            var contactInfo = await _doctorsService.GetContactInfoById(id);

            if (contactInfo == null)
                return NotFound();

            await _doctorsService.DeleteContactInfo(contactInfo);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpGet("getMyInfo")]
        public async Task<ActionResult<DoctorReadDto>> GetMyInfo()
        {
            var doctorId = User.GetClaimIntValue(ClaimType.IdDoctor);
            if (!doctorId.HasValue)
                return Unauthorized();

            var doctor = await _doctorsService.GetDoctorById(doctorId.Value);
            if (doctor == null)
                return NotFound();

            var patientReadDto = _mapper.Map<DoctorReadDto>(doctor);

            return Ok(patientReadDto);
        }
    }
}