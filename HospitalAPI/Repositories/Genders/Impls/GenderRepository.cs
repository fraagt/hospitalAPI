using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Genders.Impls
{
    public class GenderRepository : IGenderRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Gender> _genders;

        public GenderRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _genders = hospitalContext.Genders;
        }

        public async Task<IEnumerable<Gender>> GetAsync()
        {
            return await _genders.ToListAsync();
        }

        public async Task CreateAsync(Gender gender)
        {
            await _genders.AddAsync(gender);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Gender gender)
        {
            _genders.Remove(gender);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}