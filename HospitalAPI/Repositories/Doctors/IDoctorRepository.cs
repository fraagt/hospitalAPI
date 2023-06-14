using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Doctors
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAsync();
        Task<Doctor?> GetByIdAsync(int id); 
        Task<bool> HasAsync(int id);
        Task CreateAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task LoadSpecialitiesAsync(Doctor doctor);
        Task LoadServicesAsync(Doctor doctor);
    }
}