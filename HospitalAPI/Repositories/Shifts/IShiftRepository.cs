using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Shifts
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> GetAsync();
        Task CreateAsync(Shift shift);
        Task<Shift?> GetByIdAsync(int id);
        Task DeleteAsync(Shift shift);
    }
}