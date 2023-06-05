using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Allergens
{
    public interface IAllergenRepository
    {
        Task<IEnumerable<Allergen>> GetAsync();
        Task CreateAsync(Allergen allergen);
        Task<Allergen?> GetByIdAsync(int id);
        Task DeleteAsync(Allergen allergen);
    }
}