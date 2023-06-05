using HospitalAPI.Database;

namespace HospitalAPI.Services.Appointments
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<Appointment>> GetAppointments();
        Task CreateAppointment(Appointment appointment);
        Task<IEnumerable<AppointmentStatus>> GetAppointmentStatuses();
        Task<Appointment?> GetAppointmentById(int id);
        Task<AppointmentStatus?> GetAppointmentStatusById(int id);
        Task ChangeAppointmentStatus(AppointmentStatusChange appointmentStatusChange);
        Task<AppointmentStatusChange> CancelAppointment(Appointment appointment);
        Task<bool> CanBeCanceled(Appointment appointment);
        Task<IEnumerable<AppointmentStatusChange>> GetAppointmentStatusChanges(Appointment appointment);
    }
}