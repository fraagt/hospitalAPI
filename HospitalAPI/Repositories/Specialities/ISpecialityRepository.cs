using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Specialities
{
    public interface ISpecialityRepository
    {
        Task<IEnumerable<Speciality>> GetAsync();
        Task CreateAsync(Speciality speciality);
        Task<Speciality?> GetByIdAsync(int id);
        Task DeleteAsync(Speciality speciality);
    }
}