using System.Linq.Expressions;
using HospitalAPI.Database;
using HospitalAPI.Models.ContactInfos;
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

        public async Task<IEnumerable<ContactInfo>> GetAsync(ContactInfoFilter contactInfoFilter)
        {
            var query = _contactInfos.AsNoTracking();

            Expression<Func<ContactInfo, bool>> filterExpression = contactInfo => true;

            if (contactInfoFilter.DoctorId.HasValue)
            {
                query = query.Include(info => info.IdDoctors);
                filterExpression = info => info.IdDoctors.Any(d => d.IdDoctor == contactInfoFilter.DoctorId);
            }
            else if (contactInfoFilter.PatientId.HasValue)
            {
                query = query.Include(info => info.IdPatients);
                filterExpression = info => info.IdPatients.Any(p => p.IdPatient == contactInfoFilter.PatientId);
            }

            query = query.Where(filterExpression);
            
            return await query.ToListAsync();
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