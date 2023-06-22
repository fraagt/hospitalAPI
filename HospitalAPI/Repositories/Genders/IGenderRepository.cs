using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Genders
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetAsync();
        Task CreateAsync(Gender gender);
        Task RemoveAsync(Gender gender);
    }
}