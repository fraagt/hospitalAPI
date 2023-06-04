using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.ContactInfos.Impls
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<ContactInfo> _contactInfos;

        public ContactInfoRepository(
            HospitalContext hospitalContext
            )
        {
            _hospitalContext = hospitalContext;
            _contactInfos = hospitalContext.ContactInfos;
        }
        
        public async Task<IEnumerable<ContactInfo>> GetAsync()
        {
            return await _contactInfos.ToListAsync();
        }

        public async Task<ContactInfo?> GetByIdAsync(int id)
        {
            return await _contactInfos.FirstOrDefaultAsync(contactInfo => contactInfo.IdContactInfo == id);
        }

        public async Task CreateAsync(ContactInfo contactInfo)
        {
            await _contactInfos.AddAsync(contactInfo);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ContactInfo contactInfo)
        {
            _contactInfos.Remove(contactInfo);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}