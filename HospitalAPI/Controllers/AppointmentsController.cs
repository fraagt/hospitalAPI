using AutoMapper;
using HospitalAPI.Models.Appointments;
using HospitalAPI.Services.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IMapper _mapper;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IMapper mapper
        )
        {
            _appointmentsService = appointmentsService;
            _mapper = mapper;
        }

        [HttpGet("getAppointments")]
        public async Task<ActionResult<AppointmentReadDto>> GetAppointments()
        {
            var appointments = await _appointmentsService.GetAppointments();

            var appointmentReadDtos = _mapper.Map<IEnumerable<AppointmentReadDto>>(appointments);

            return Ok(appointmentReadDtos);
        }
    }
}