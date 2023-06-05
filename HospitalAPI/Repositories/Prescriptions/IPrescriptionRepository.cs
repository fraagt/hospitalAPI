using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Prescriptions
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAsync();
        Task<Prescription?> GetByIdAsync(int id);
        Task CreateAsync(Prescription prescription);
        Task UpdateAsync(Prescription prescription);
        Task DeleteAsync(Prescription prescription);
    }
}