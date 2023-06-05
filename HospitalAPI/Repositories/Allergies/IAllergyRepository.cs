using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Allergies
{
    public interface IAllergyRepository
    {
        Task<IEnumerable<Allergy>> GetAsync();
        Task<Allergy?> GetByIdAsync(int id);
        Task CreateAsync(Allergy allergy);
        Task UpdateAsync(Allergy allergy);
        Task DeleteAsync(Allergy allergy);
    }
}