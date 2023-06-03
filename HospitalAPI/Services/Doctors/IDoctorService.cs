using HospitalAPI.Database;

namespace HospitalAPI.Services.Doctors
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor?> GetDoctorById(int id);
        Task<bool> HasDoctor(int id);
        Task UpdateDoctor(Doctor doctor);
        Task<IEnumerable<WorkHistory>> GetWorkHistories();
        Task CreateWorkHistory(WorkHistory workHistory);
    }
}