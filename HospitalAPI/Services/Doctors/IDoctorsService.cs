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
        Task<IEnumerable<AppointmentTime>> GetAppointmentTimes();
        Task CreateAppointmentTime(AppointmentTime appointmentTime);
        Task<AppointmentTime?> GetAppointmentTimeById(int id);
        Task DeleteAppointmentTime(AppointmentTime appointmentTime);
        Task<IEnumerable<Service>> GetServices();
        Task<Service?> GetServiceById(int id);
        Task CreateService(Service service);
        Task DeleteService(Service service);
        Task<IEnumerable<Shift>> GetShifts();
        Task<Shift?> GetShiftById(int id);
        Task CreateShift(Shift shift);
        Task DeleteShift(Shift shift);
        Task<IEnumerable<ContactInfo>> GetContactInfosByDoctor(Doctor doctor);
        Task<ContactInfo?> GetContactInfoById(int id);
        Task CreateContactInfo(ContactInfo contactInfo);
        Task DeleteContactInfo(ContactInfo contactInfo);
        
        
        Task LoadDoctorSpecialities(Doctor doctor);
        Task LoadDoctorServices(Doctor doctor);
    }
}