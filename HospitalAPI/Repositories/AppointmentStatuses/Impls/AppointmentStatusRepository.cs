using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.AppointmentStatuses.Impls
{
    public class AppointmentStatusRepository : IAppointmentStatusRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<AppointmentStatus> _appointmentStatuses;

        public AppointmentStatusRepository(
            HospitalContext hospitalContext
            )
        {
            _hospitalContext = hospitalContext;
            _appointmentStatuses = hospitalContext.AppointmentStatuses;
        }

        public async Task<IEnumerable<AppointmentStatus>> GetAsync()
        {
            return await _appointmentStatuses.ToListAsync();
        }

        public async Task<AppointmentStatus?> GetByIdAsync(int id)
        {
            return await _appointmentStatuses.FirstOrDefaultAsync(status => status.IdAppointmentStatus == id);
        }

        public async Task<AppointmentStatus?> GetByTitleAsync(string title)
        {
            return await _appointmentStatuses.FirstOrDefaultAsync(status => status.Title.Equals(title));
        }
    }
}