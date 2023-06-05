using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Allergens.Impls
{
    public class AllergenRepository : IAllergenRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Allergen> _allergens;

        public AllergenRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _allergens = hospitalContext.Allergens;
        }

        public async Task<IEnumerable<Allergen>> GetAsync()
        {
            return await _allergens.ToListAsync();
        }

        public async Task CreateAsync(Allergen allergen)
        {
            await _allergens.AddAsync(allergen);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<Allergen?> GetByIdAsync(int id)
        {
            return await _allergens.FirstOrDefaultAsync(allergen => allergen.IdAllergen == id);
        }

        public async Task DeleteAsync(Allergen allergen)
        {
            _allergens.Remove(allergen);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}