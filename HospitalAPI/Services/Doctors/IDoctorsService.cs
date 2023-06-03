using HospitalAPI.Database;

namespace HospitalAPI.Services.Doctors
{
    public interface IDoctorsService
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor?> GetDoctorById(int id);
        Task<bool> HasDoctor(int id);
        Task UpdateDoctor(Doctor doctor);
        Task<IEnumerable<WorkHistory>> GetWorkHistories();
        Task CreateWorkHistory(WorkHistory workHistory);
        Task<bool> HasWorkHistory(int id);
        Task<WorkHistory?> GetWorkHistoryById(int id);
        Task DeleteWorkHistory(WorkHistory workHistory);
        Task<IEnumerable<Speciality>> GetSpecialities();
        Task CreateSpeciality(Speciality speciality);
        Task<Speciality?> GetSpecialityById(int id);
        Task DeleteSpeciality(Speciality speciality);
        
        
        Task LoadDoctorSpecialities(Doctor doctor);
    }
}