using HospitalAPI.Database;

namespace HospitalAPI.Repositories.MedicalRecords
{
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetAsync();
        Task<MedicalRecord?> GetByIdAsync(int id);
        Task CreateAsync(MedicalRecord medicalRecord);
        Task UpdateAsync(MedicalRecord medicalRecord);
        Task DeleteAsync(MedicalRecord medicalRecord);
    }
}