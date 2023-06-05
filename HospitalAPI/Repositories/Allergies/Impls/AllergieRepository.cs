using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Allergies.Impls
{
    public class AllergyRepository : IAllergyRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Allergy> _allergies;

        public AllergyRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _allergies = hospitalContext.Allergies;
        }

        public async Task<IEnumerable<Allergy>> GetAsync()
        {
            return await _allergies.ToListAsync();
        }

        public async Task<Allergy?> GetByIdAsync(int id)
        {
            return await _allergies.FirstOrDefaultAsync(p => p.IdAllergy == id);
        }

        public async Task CreateAsync(Allergy allergy)
        {
            await _allergies.AddAsync(allergy);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Allergy allergy)
        {
            _allergies.Entry(allergy).State = EntityState.Modified;
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Allergy allergy)
        {
            _allergies.Remove(allergy);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}