using HospitalAPI.Database;
using HospitalAPI.Repositories.Appointments;

namespace HospitalAPI.Services.Appointments.Impls
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsService(
            IAppointmentRepository appointmentRepository
            )
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _appointmentRepository.GetAsync();
        }
    }
}