using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;

namespace HospitalAPI.Services.Appointments
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<Appointment>> GetAppointments(AppointmentFilters filters);
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