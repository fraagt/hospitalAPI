using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Prescriptions.Impls
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Prescription> _prescriptions;

        public PrescriptionRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _prescriptions = hospitalContext.Prescriptions;
        }

        public async Task<IEnumerable<Prescription>> GetAsync()
        {
            return await _prescriptions.ToListAsync();
        }

        public async Task<Prescription?> GetByIdAsync(int id)
        {
            return await _prescriptions.FirstOrDefaultAsync(p => p.IdPrescription == id);
        }

        public async Task CreateAsync(Prescription prescription)
        {
            await _prescriptions.AddAsync(prescription);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Prescription prescription)
        {
            _prescriptions.Entry(prescription).State = EntityState.Modified;
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prescription prescription)
        {
            _prescriptions.Remove(prescription);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}