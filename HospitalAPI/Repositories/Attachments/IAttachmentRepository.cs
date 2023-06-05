using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Attachments
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAsync();
        Task<Attachment?> GetByIdAsync(int id);
        Task CreateAsync(Attachment attachment);
        Task UpdateAsync(Attachment attachment);
        Task DeleteAsync(Attachment attachment);
    }
}