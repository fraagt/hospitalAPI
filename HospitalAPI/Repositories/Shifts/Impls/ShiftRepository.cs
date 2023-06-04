using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Shifts.Impls
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Shift> _shifts;

        public ShiftRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _shifts = hospitalContext.Shifts;
        }

        public async Task<IEnumerable<Shift>> GetAsync()
        {
            return await _shifts.ToListAsync();
        }

        public async Task CreateAsync(Shift shift)
        {
            await _shifts.AddAsync(shift);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<Shift?> GetByIdAsync(int id)
        {
            return await _shifts.FirstOrDefaultAsync(shift => shift.IdShift == id);
        }

        public async Task DeleteAsync(Shift shift)
        {
            _shifts.Remove(shift);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}