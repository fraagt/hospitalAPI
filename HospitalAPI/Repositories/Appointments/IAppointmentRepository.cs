using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Appointments
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAsync();
    }
}