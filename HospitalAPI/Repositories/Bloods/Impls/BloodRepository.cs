using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Bloods.Impls
{
    public class BloodRepository : IBloodRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Blood> _bloods;

        public BloodRepository(
            HospitalContext hospitalContext
            )
        {
            _hospitalContext = hospitalContext;
            _bloods = hospitalContext.Bloods;
        }
        
        public async Task<IEnumerable<Blood>> GetAsync()
        {
            return await _bloods.ToListAsync();
        }

        public async Task<Blood?> GetByIdAsync(int id)
        {
            return await _bloods.FirstOrDefaultAsync(b => b.IdBlood == id);
        }

        public async Task CreateAsync(Blood blood)
        {
            await _bloods.AddAsync(blood);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blood blood)
        {
            _bloods.Remove(blood);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}