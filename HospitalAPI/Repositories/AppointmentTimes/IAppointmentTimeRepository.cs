using HospitalAPI.Database;

namespace HospitalAPI.Repositories.AppointmentTimes
{
    public interface IAppointmentTimeRepository
    {
        Task<IEnumerable<AppointmentTime>> GetAsync();
        Task<AppointmentTime?> GetByIdAsync(int id);
        Task CreateAsync(AppointmentTime appointmentTime);
        Task DeleteAsync(AppointmentTime appointmentTime);
    }
}