using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;
using HospitalAPI.Models.AppointmentStatusChanges;
using HospitalAPI.Repositories.Appointments;
using HospitalAPI.Repositories.AppointmentStatusChanges;
using HospitalAPI.Repositories.AppointmentStatuses;
using HospitalAPI.Repositories.AppointmentTimes;

namespace HospitalAPI.Services.Appointments.Impls
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentStatusRepository _appointmentStatusRepository;
        private readonly IAppointmentStatusChangeRepository _appointmentStatusChangeRepository;
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;

        public AppointmentsService(
            IAppointmentRepository appointmentRepository,
            IAppointmentStatusRepository appointmentStatusRepository,
            IAppointmentStatusChangeRepository appointmentStatusChangeRepository,
            IAppointmentTimeRepository appointmentTimeRepository
        )
        {
            _appointmentRepository = appointmentRepository;
            _appointmentStatusRepository = appointmentStatusRepository;
            _appointmentStatusChangeRepository = appointmentStatusChangeRepository;
            _appointmentTimeRepository = appointmentTimeRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(AppointmentFilters filters)
        {
            return await _appointmentRepository.GetAsync(filters);
        }

        public async Task CreateAppointment(Appointment appointment)
        {
            await _appointmentRepository.CreateAsync(appointment);
        }

        public async Task<IEnumerable<AppointmentStatus>> GetAppointmentStatuses()
        {
            return await _appointmentStatusRepository.GetAsync();
        }

        public async Task<Appointment?> GetAppointmentById(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<AppointmentStatus?> GetAppointmentStatusById(int id)
        {
            return await _appointmentStatusRepository.GetByIdAsync(id);
        }

        public async Task ChangeAppointmentStatus(AppointmentStatusChange appointmentStatusChange)
        {
            await _appointmentStatusChangeRepository.CreateAsync(appointmentStatusChange);
        }

        public async Task<AppointmentStatusChange> CancelAppointment(Appointment appointment)
        {
            var canceledStatus = await _appointmentStatusRepository.GetByTitleAsync("Canceled");

            var appointmentStatusChange = new AppointmentStatusChange
            {
                IdAppointmentNavigation = appointment,
                IdAppointmentStatusNavigation = canceledStatus!
            };

            await _appointmentStatusChangeRepository.CreateAsync(appointmentStatusChange);

            // await _appointmentRepository.LoadAppointmentTime(appointment);
            appointment.IdAppointmentTimeNavigation.Reserved = false;
            await _appointmentRepository.UpdateAsync(appointment);

            return appointmentStatusChange;
        }

        public Task<bool> CanBeCanceled(Appointment appointment)
        {
            // var changes = await GetAppointmentStatusChanges(appointment);
            //
            // var canceledStatus = await _appointmentStatusRepository.GetByTitleAsync("Canceled");

            var isCanceled = appointment.AppointmentStatusChanges.Any(change => change.IdAppointmentStatusNavigation.Title.Equals("Canceled"));
            var dateDiffer = appointment.IdAppointmentTimeNavigation.Date.Millisecond - DateTime.Today.Millisecond;
            var isExpired = dateDiffer >= 0;

            return  Task.FromResult(!isCanceled && !isExpired);
        }

        public async Task<IEnumerable<AppointmentStatusChange>> GetAppointmentStatusChanges(Appointment appointment)
        {
            var filter = new AppointmentStatusChangeFilter {AppointmentId = appointment.IdAppointment};
            return await _appointmentStatusChangeRepository.GetAsync(filter);
        }
    }
}