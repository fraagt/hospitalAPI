using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Patients.Impls
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Patient> _patients;

        public PatientRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _patients = hospitalContext.Patients;
        }

        public async Task<IEnumerable<Patient>> GetAsync()
        {
            return await _patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _patients.FirstOrDefaultAsync(patient => patient.IdPatient == id);
        }

        public async Task Update(Patient patient)
        {
            _patients.Entry(patient)
                .State = EntityState.Modified;

            await _hospitalContext.SaveChangesAsync();
        }
    }
}