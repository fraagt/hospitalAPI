using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;

namespace HospitalAPI.Repositories.Appointments
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAsync(AppointmentFilters filters);
        Task CreateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int id);
        Task LoadAppointmentTime(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}