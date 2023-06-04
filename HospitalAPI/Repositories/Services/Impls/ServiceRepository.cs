using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Services.Impls
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Service> _services;

        public ServiceRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _services = hospitalContext.Services;
        }
        
        public async Task<IEnumerable<Service>> GetAsync()
        {
            return await _services.ToListAsync();
        }

        public async Task CreateAsync(Service service)
        {
            await _services.AddAsync(service);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _services.FirstOrDefaultAsync(service => service.IdService == id);
        }

        public async Task DeleteAsync(Service service)
        {
            _services.Remove(service);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}