using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Services
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAsync();
        Task CreateAsync(Service service);
        Task<Service?> GetByIdAsync(int id);
        Task DeleteAsync(Service service);
    }
}