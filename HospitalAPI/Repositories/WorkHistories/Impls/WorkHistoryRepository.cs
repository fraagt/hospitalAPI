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
        
        public async Task<IEnumerable<WorkHistory>> GetAsync()
        {
            return await _workHistories.ToListAsync();
        }

        public async Task CreateAsync(WorkHistory workHistory)
        {
            await _workHistories.AddAsync(workHistory);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<bool> HasAsync(int id)
        {
            return await _workHistories.AnyAsync(workHistory => workHistory.IdWorkHistory == id);
        }

        public async Task<WorkHistory?> GetByIdAsync(int id)
        {
            return await _workHistories.FirstOrDefaultAsync(workHistory => workHistory.IdWorkHistory == id);
        }

        public async Task DeleteAsync(WorkHistory workHistory)
        {
            _workHistories.Remove(workHistory);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}