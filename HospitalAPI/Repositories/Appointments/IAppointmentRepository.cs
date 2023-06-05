using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Appointments
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAsync();
        Task CreateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int id);
        Task LoadAppointmentTime(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}