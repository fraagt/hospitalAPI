using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.MedicalRecords.Impls
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<MedicalRecord> _medicalRecords;

        public MedicalRecordRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _medicalRecords = hospitalContext.MedicalRecords;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAsync()
        {
            return await _medicalRecords.ToListAsync();
        }

        public async Task<MedicalRecord?> GetByIdAsync(int id)
        {
            return await _medicalRecords.FirstOrDefaultAsync(record => record.IdMedicalRecord == id);
        }

        public async Task CreateAsync(MedicalRecord medicalRecord)
        {
            await _medicalRecords.AddAsync(medicalRecord);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicalRecord medicalRecord)
        {
            _medicalRecords.Entry(medicalRecord).State = EntityState.Modified;
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(MedicalRecord medicalRecord)
        {
            _medicalRecords.Remove(medicalRecord);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}