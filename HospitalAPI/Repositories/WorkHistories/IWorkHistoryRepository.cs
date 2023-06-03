using HospitalAPI.Database;

namespace HospitalAPI.Repositories.WorkHistories
{
    public interface IWorkHistoryRepository
    {
        Task<IEnumerable<WorkHistory>> GetAsync();
        Task CreateAsync(WorkHistory workHistory);
        Task<bool> HasAsync(int id);
        Task<WorkHistory?> GetByIdAsync(int id);
        Task DeleteAsync(WorkHistory workHistory);
    }
}