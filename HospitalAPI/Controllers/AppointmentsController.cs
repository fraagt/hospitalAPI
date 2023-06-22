using System.Collections;
using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;
using HospitalAPI.Models.AppointmentStatusChanges;
using HospitalAPI.Models.AppointmentStatuses;
using HospitalAPI.Models.AppointmentTimes;
using HospitalAPI.Models.Services;
using HospitalAPI.Services.Appointments;
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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IPatientsService _patientsService;
        private readonly IMapper _mapper;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IPatientsService patientsService,
            IMapper mapper
        )
        {
            _appointmentsService = appointmentsService;
            _patientsService = patientsService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("getAppointments")]
        public async Task<ActionResult<AppointmentReadDto>> GetAppointments()
        {
            var userIdRole = User.GetClaimIntValue(ClaimType.IdRole);
            if (!userIdRole.HasValue)
                return Unauthorized("Not authorized user");
            var userRole = RolesExtension.ToUserRole(userIdRole.Value);
            var filters = new AppointmentFilters();

            if (userRole != EUserRole.Administrator)
            {
                var idProfileValue = User.GetClaimIntValue(ClaimType.IdProfile);
                if (!idProfileValue.HasValue)
                    return Unauthorized();
                switch (userRole)
                {
                    case EUserRole.Patient:
                        filters.IdPatient = idProfileValue.Value;
                        break;
                    case EUserRole.Doctor:
                        filters.IdDoctor = idProfileValue.Value;
                        break;
                }
            }

            var appointments = await _appointmentsService.GetAppointments(filters);

            var appointmentReadDtos = appointments.Select(MapToReadDto);

            return Ok(appointmentReadDtos);
        }

        private AppointmentReadDto MapToReadDto(Appointment appointment)
        {
            return new AppointmentReadDto
            {
                IdAppointment = appointment.IdAppointment,
                IdPatient = appointment.IdPatient,
                IdDoctor = appointment.IdDoctor,
                AppointmentTime = _mapper.Map<AppointmentTimeReadDto>(appointment.IdAppointmentTimeNavigation),
                Service = _mapper.Map<ServiceReadDto>(appointment.IdServiceNavigation),
                CurrentStatus =
                    _mapper.Map<AppointmentStatusReadDto>(
                        appointment.AppointmentStatusChanges.Max(change => change.CreatedAt.Millisecond)),
                StatusChanges = appointment.AppointmentStatusChanges.Select(MapToReadDto)
            };
        }

        private AppointmentStatusChangeReadDto MapToReadDto(AppointmentStatusChange change)
        {
            return new AppointmentStatusChangeReadDto
            {
                IdAppointmentStatusChange = change.IdAppointmentStatusChange,
                IdAppointment = change.IdAppointment,
                AppointmentStatus = _mapper.Map<AppointmentStatusReadDto>(change.IdAppointmentStatusNavigation),
                CreatedAt = change.CreatedAt
            };
        }

        [RoleAuthorize(EUserRole.Patient)]
        [HttpPost("createAppointment")]
        public async Task<ActionResult<AppointmentReadDto>> CreateAppointment(AppointmentCreateDto appointmentCreateDto)
        {
            //TODO it is needed to take logined patient first and than create a contactInfo for him
            var patientId = 1;
            var patient = await _patientsService.GetPatientById(patientId);

            var appointment = _mapper.Map<Appointment>(appointmentCreateDto);
            appointment.IdPatientNavigation = patient!;

            await _appointmentsService.CreateAppointment(appointment);

            var appointmentReadDto = _mapper.Map<AppointmentReadDto>(appointment);
            return Created(string.Empty, appointmentReadDto);
        }

        [HttpGet("getAppointmentStatuses")]
        public async Task<ActionResult<AppointmentReadDto>> GetAppointmentStatuses()
        {
            var appointmentStatuses = await _appointmentsService.GetAppointmentStatuses();

            var appointmentStatusesReadDtos = _mapper.Map<IEnumerable<AppointmentReadDto>>(appointmentStatuses);

            return Ok(appointmentStatusesReadDtos);
        }

        [HttpGet("getAppointmentStatusChanges")]
        public async Task<ActionResult<AppointmentStatusChangeReadDto>> GetAppointmentStatusChanges(int appointmentId)
        {
            var appointment = await _appointmentsService.GetAppointmentById(appointmentId);
            if (appointment == null)
                return BadRequest("Appointment not found");

            var appointmentStatusChanges =
                await _appointmentsService.GetAppointmentStatusChanges(appointment);

            var appointmentStatusesReadDtos =
                _mapper.Map<IEnumerable<AppointmentStatusChangeReadDto>>(appointmentStatusChanges);

            return Ok(appointmentStatusesReadDtos);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("changeAppointmentStatus")]
        public async Task<ActionResult<AppointmentStatusChangeReadDto>> ChangeAppointmentStatus(
            AppointmentStatusChangeCreateDto appointmentStatusChangeCreateDto)
        {
            var appointmentStatusChange = _mapper.Map<AppointmentStatusChange>(appointmentStatusChangeCreateDto);

            var appointment =
                await _appointmentsService.GetAppointmentById(appointmentStatusChange.IdAppointment);
            if (appointment == null)
                return BadRequest("Appointment not found");

            var appointmentStatus =
                await _appointmentsService.GetAppointmentStatusById(
                    appointmentStatusChange.IdAppointmentStatus);
            if (appointmentStatus == null)
                return BadRequest("Appointment status not found");

            await _appointmentsService.ChangeAppointmentStatus(appointmentStatusChange);

            var appointmentStatusChangeReadDto = _mapper.Map<AppointmentStatusChangeReadDto>(appointmentStatusChange);
            return Created(string.Empty, appointmentStatusChangeReadDto);
        }

        [RoleAuthorize(EUserRole.Patient)]
        [HttpPost("cancelAppointment")]
        public async Task<ActionResult<AppointmentStatusChangeReadDto>> CancelAppointment(int appointmentId)
        {
            var appointment = await _appointmentsService.GetAppointmentById(appointmentId);
            if (appointment == null)
                return BadRequest("Appointment not found");

            if (!await _appointmentsService.CanBeCanceled(appointment))
                return BadRequest("Appointment cannot be canceled");

            var appointmentStatusChange = await _appointmentsService.CancelAppointment(appointment);

            var appointmentStatusChangeReadDto = _mapper.Map<AppointmentStatusChangeReadDto>(appointmentStatusChange);
            return Created(string.Empty, appointmentStatusChangeReadDto);
        }
    }
}