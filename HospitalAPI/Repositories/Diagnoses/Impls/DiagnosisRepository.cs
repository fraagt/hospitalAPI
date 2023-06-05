using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Diagnoses.Impls
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Diagnosis> _diagnoses;

        public DiagnosisRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _diagnoses = hospitalContext.Diagnoses;
        }

        public async Task<IEnumerable<Diagnosis>> GetAsync()
        {
            return await _diagnoses.ToListAsync();
        }

        public async Task<Diagnosis?> GetByIdAsync(int id)
        {
            return await _diagnoses.FirstOrDefaultAsync(p => p.IdDiagnosis == id);
        }

        public async Task CreateAsync(Diagnosis diagnosis)
        {
            await _diagnoses.AddAsync(diagnosis);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Diagnosis diagnosis)
        {
            _diagnoses.Entry(diagnosis).State = EntityState.Modified;
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Diagnosis diagnosis)
        {
            _diagnoses.Remove(diagnosis);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}