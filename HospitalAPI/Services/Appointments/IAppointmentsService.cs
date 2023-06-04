using HospitalAPI.Database;

namespace HospitalAPI.Services.Appointments
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<Appointment>> GetAppointments();
    }
}