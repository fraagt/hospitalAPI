using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentTimes;
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

        public async Task<IEnumerable<AppointmentTime>> GetAsync(AppointmentTimeFilters filters)
        {
            var query = _appointmentTimes.AsQueryable();

            if (filters.DoctorId.HasValue)
            {
                query = query.Where(appointmentTime =>
                    appointmentTime.IdDoctor.Equals(filters.DoctorId.Value));
            }

            if (filters.Date.HasValue)
            {
                query = query.Where(appointmentTime => appointmentTime.Date.CompareTo(filters.Date.Value) == 0);
            }

            return await query.ToListAsync();
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