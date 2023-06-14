using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Patients
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task CreateAsync(Patient patient);
        Task Update(Patient patient);
    }
}