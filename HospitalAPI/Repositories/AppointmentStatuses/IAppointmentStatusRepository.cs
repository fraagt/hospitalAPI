using HospitalAPI.Database;

namespace HospitalAPI.Repositories.AppointmentStatuses
{
    public interface IAppointmentStatusRepository
    {
        Task<IEnumerable<AppointmentStatus>> GetAsync();
        Task<AppointmentStatus?> GetByIdAsync(int id);
        Task<AppointmentStatus?> GetByTitleAsync(string title);
    }
}