using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentStatusChanges;

namespace HospitalAPI.Repositories.AppointmentStatusChanges
{
    public interface IAppointmentStatusChangeRepository
    {
        Task CreateAsync(AppointmentStatusChange appointmentStatusChange);
        Task<IEnumerable<AppointmentStatusChange>> GetAsync(AppointmentStatusChangeFilter filter);
    }
}