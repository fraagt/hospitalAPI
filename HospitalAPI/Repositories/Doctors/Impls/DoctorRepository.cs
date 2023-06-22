using HospitalAPI.Database;
using HospitalAPI.Models.Doctors;
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

        public async Task<IEnumerable<Doctor>> GetAsync(GetDoctorsFilters filters)
        {
            var query = _doctors.AsQueryable();

            if (filters.IdSpeciality.HasValue)
            {
                query = query.Where(doctor =>
                    doctor.IdSpecialities.Any(speciality => speciality.IdSpeciality == filters.IdSpeciality));
            }

            if (filters.AppointmentAvailableDate.HasValue)
            {
                query = query.Where(doctor =>
                    doctor.AppointmentTimes.Any(time =>
                        time.Date.Equals(filters.AppointmentAvailableDate) && !time.Reserved));
            }

            return await query.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _doctors.FirstOrDefaultAsync(doctor => doctor.IdDoctor == id);
        }

        public async Task<bool> HasAsync(int id)
        {
            return await _doctors.AnyAsync(doctor => doctor.IdDoctor == id);
        }

        public async Task CreateAsync(Doctor doctor)
        {
            await _doctors.AddAsync(doctor);
            await _hospitalContext.SaveChangesAsync();
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