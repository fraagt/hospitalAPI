using System.Linq.Expressions;
using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentStatusChanges;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.AppointmentStatusChanges.Impls
{
    public class AppointmentStatusChangeRepository : IAppointmentStatusChangeRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<AppointmentStatusChange> _appointmentStatusChanges;

        public AppointmentStatusChangeRepository(
            HospitalContext hospitalContext
            )
        {
            _hospitalContext = hospitalContext;
            _appointmentStatusChanges = hospitalContext.AppointmentStatusChanges;
        }

        public async Task CreateAsync(AppointmentStatusChange appointmentStatusChange)
        {
            await _appointmentStatusChanges.AddAsync(appointmentStatusChange);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppointmentStatusChange>> GetAsync(AppointmentStatusChangeFilter filter)
        {
            var query = _appointmentStatusChanges.AsNoTracking();

            Expression<Func<AppointmentStatusChange, bool>> filterExpression = change => true;

            if (filter.AppointmentId.HasValue)
            {
                filterExpression = change => change.IdAppointment == filter.AppointmentId;
            }

            query = query.Where(filterExpression);

            return await query.ToListAsync();
        }
    }
}