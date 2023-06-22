using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentTimes;

namespace HospitalAPI.Repositories.AppointmentTimes
{
    public interface IAppointmentTimeRepository
    {
        Task<IEnumerable<AppointmentTime>> GetAsync(AppointmentTimeFilters filters);
        Task<AppointmentTime?> GetByIdAsync(int id);
        Task CreateAsync(AppointmentTime appointmentTime);
        Task DeleteAsync(AppointmentTime appointmentTime);
    }
}