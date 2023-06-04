using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Doctors.Impls
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Doctor> _doctors;

        public DoctorRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _doctors = hospitalContext.Doctors;
        }

        public async Task<IEnumerable<Doctor>> GetAsync()
        {
            return await _doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _doctors.FirstOrDefaultAsync(doctor => doctor.IdDoctor == id);
        }

        public async Task<bool> HasAsync(int id)
        {
            return await _doctors.AnyAsync(doctor => doctor.IdDoctor == id);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _doctors.Entry(doctor).State = EntityState.Modified;
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task LoadSpecialitiesAsync(Doctor doctor)
        {
            await _doctors.Entry(doctor)
                .Collection(d => d.IdSpecialities)
                .LoadAsync();
        }

        public async Task LoadServicesAsync(Doctor doctor)
        {
            await _doctors.Entry(doctor)
                .Collection(d => d.IdServices)
                .LoadAsync();
        }
    }
}