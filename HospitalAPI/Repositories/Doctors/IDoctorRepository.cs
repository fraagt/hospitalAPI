using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Doctors
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id); 
        Task<bool> HasDoctorAsync(int id);
        Task UpdateDoctorAsync(Doctor doctor);
    }
}