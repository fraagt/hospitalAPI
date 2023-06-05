using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Bloods
{
    public interface IBloodRepository
    {
        Task<IEnumerable<Blood>> GetAsync();
        Task<Blood?> GetByIdAsync(int id);
        Task CreateAsync(Blood blood);
        Task DeleteAsync(Blood blood);
    }
}