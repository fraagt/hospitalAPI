using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Specialities.Impls
{
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Speciality> _specialities;

        public SpecialityRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _specialities = hospitalContext.Specialities;
        }

        public async Task<IEnumerable<Speciality>> GetAsync()
        {
            return await _specialities.ToListAsync();
        }

        public async Task CreateAsync(Speciality speciality)
        {
            await _specialities.AddAsync(speciality);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<Speciality?> GetByIdAsync(int id)
        {
            return await _specialities.FirstOrDefaultAsync(speciality => speciality.IdSpeciality == id);
        }

        public async Task DeleteAsync(Speciality speciality)
        {
            _specialities.Remove(speciality);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}