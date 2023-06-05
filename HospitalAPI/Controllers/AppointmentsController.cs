using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;
using HospitalAPI.Models.AppointmentStatusChanges;
using HospitalAPI.Services.Appointments;
using HospitalAPI.Services.Patients;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
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

        [HttpGet("getAppointments")]
        public async Task<ActionResult<AppointmentReadDto>> GetAppointments()
        {
            var appointments = await _appointmentsService.GetAppointments();

            var appointmentReadDtos = _mapper.Map<IEnumerable<AppointmentReadDto>>(appointments);

            return Ok(appointmentReadDtos);
        }

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

            var appointmentStatusesReadDtos = _mapper.Map<IEnumerable<AppointmentStatusChangeReadDto>>(appointmentStatusChanges);

            return Ok(appointmentStatusesReadDtos);
        }

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