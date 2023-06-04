using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.AppointmentTimes.Impls
{
    public class AppointmentTimeRepository : IAppointmentTimeRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<AppointmentTime> _appointmentTimes;

        public AppointmentTimeRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _appointmentTimes = hospitalContext.AppointmentTimes;
        }
        
        public async Task<IEnumerable<AppointmentTime>> GetAsync()
        {
            return await _appointmentTimes.ToListAsync();
        }

        public async Task<AppointmentTime?> GetByIdAsync(int id)
        {
            return await _appointmentTimes.FirstOrDefaultAsync(appTime => appTime.IdAppointmentTime == id);
        }

        public async Task CreateAsync(AppointmentTime appointmentTime)
        {
            await _appointmentTimes.AddAsync(appointmentTime);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AppointmentTime appointmentTime)
        {
            _appointmentTimes.Remove(appointmentTime);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}