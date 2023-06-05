using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Attachments.Impls
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Attachment> _attachments;

        public AttachmentRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _attachments = hospitalContext.Attachments;
        }

        public async Task<IEnumerable<Attachment>> GetAsync()
        {
            return await _attachments.ToListAsync();
        }

        public async Task<Attachment?> GetByIdAsync(int id)
        {
            return await _attachments.FirstOrDefaultAsync(p => p.IdAttachment == id);
        }

        public async Task CreateAsync(Attachment attachment)
        {
            await _attachments.AddAsync(attachment);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attachment attachment)
        {
            _attachments.Entry(attachment).State = EntityState.Modified;
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Attachment attachment)
        {
            _attachments.Remove(attachment);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}