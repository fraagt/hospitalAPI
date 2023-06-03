using HospitalAPI.Database;

namespace HospitalAPI.Repositories.WorkHistories
{
    public interface IWorkHistoryRepository
    {
        Task<IEnumerable<WorkHistory>> GetWorkHistoriesAsync();
        Task CreateWorkHistoryAsync(WorkHistory workHistory);
    }
}