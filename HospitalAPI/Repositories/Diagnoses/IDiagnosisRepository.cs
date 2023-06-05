using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Diagnoses
{
    public interface IDiagnosisRepository
    {
        Task<IEnumerable<Diagnosis>> GetAsync();
        Task<Diagnosis?> GetByIdAsync(int id);
        Task CreateAsync(Diagnosis diagnosis);
        Task UpdateAsync(Diagnosis diagnosis);
        Task DeleteAsync(Diagnosis diagnosis);
    }
}