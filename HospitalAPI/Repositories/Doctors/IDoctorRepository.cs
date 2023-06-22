using HospitalAPI.Database;
using HospitalAPI.Models.Doctors;

namespace HospitalAPI.Repositories.Doctors
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAsync(GetDoctorsFilters filters);
        Task<Doctor?> GetByIdAsync(int id); 
        Task<bool> HasAsync(int id);
        Task CreateAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task LoadSpecialitiesAsync(Doctor doctor);
        Task LoadServicesAsync(Doctor doctor);
    }
}