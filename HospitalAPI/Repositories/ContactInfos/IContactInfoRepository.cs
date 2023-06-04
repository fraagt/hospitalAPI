using HospitalAPI.Database;
using HospitalAPI.Models.ContactInfos;

namespace HospitalAPI.Repositories.ContactInfos
{
    public interface IContactInfoRepository
    {
        Task<IEnumerable<ContactInfo>> GetAsync(ContactInfoFilter contactInfoFilter);
        Task<ContactInfo?> GetByIdAsync(int id);
        Task CreateAsync(ContactInfo contactInfo);
        Task DeleteAsync(ContactInfo contactInfo);
    }
}