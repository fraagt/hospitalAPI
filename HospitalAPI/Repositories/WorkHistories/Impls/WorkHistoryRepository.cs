using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.WorkHistories.Impls
{
    public class WorkHistoryRepository : IWorkHistoryRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<WorkHistory> _workHistories;

        public WorkHistoryRepository(
            HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
            _workHistories = hospitalContext.WorkHistories;
        }
        
        public async Task<IEnumerable<WorkHistory>> GetWorkHistoriesAsync()
        {
            return await _workHistories.ToListAsync();
        }

        public async Task CreateWorkHistoryAsync(WorkHistory workHistory)
        {
            await _workHistories.AddAsync(workHistory);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}