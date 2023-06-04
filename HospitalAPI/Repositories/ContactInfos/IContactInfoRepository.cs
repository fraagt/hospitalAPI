using HospitalAPI.Database;

namespace HospitalAPI.Repositories.ContactInfos
{
    public interface IContactInfoRepository
    {
        Task<IEnumerable<ContactInfo>> GetAsync();
        Task<ContactInfo?> GetByIdAsync(int id);
        Task CreateAsync(ContactInfo contactInfo);
        Task DeleteAsync(ContactInfo contactInfo);
    }
}