using System.Linq.Expressions;
using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Appointments.Impls
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<Appointment> _appointments;

        public AppointmentRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _appointments = hospitalContext.Appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAsync(AppointmentFilters filters)
        {
            var query = _appointments.AsQueryable();
            Expression<Func<Appointment, bool>>? combinedExpression = null;

            if (filters.IdPatient.HasValue)
            {
                Expression<Func<Appointment, bool>> patientExpression = app => app.IdPatient == filters.IdPatient.Value;
                combinedExpression = combinedExpression == null ? patientExpression : CombineExpressions(combinedExpression, patientExpression);
            }

            if (filters.IdDoctor.HasValue)
            {
                Expression<Func<Appointment, bool>> doctorExpression = app => app.IdDoctor == filters.IdDoctor.Value;
                combinedExpression = combinedExpression == null ? doctorExpression : CombineExpressions(combinedExpression, doctorExpression);
            }

            if (combinedExpression != null)
            {
                query = query.Where(combinedExpression);
            }

            return await query.ToListAsync();
        }

        private Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>>? expression1, Expression<Func<T, bool>>? expression2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var combinedBody = Expression.AndAlso(
                Expression.Invoke(expression1, parameter),
                Expression.Invoke(expression2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
        }

        public async Task CreateAsync(Appointment appointment)
        {
            await _appointments.AddAsync(appointment);
            await _hospitalContext.SaveChangesAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _appointments.FirstOrDefaultAsync(appointment => appointment.IdAppointment == id);
        }

        public async Task LoadAppointmentTime(Appointment appointment)
        {
            await _appointments.Entry(appointment)
                .Reference(a => a.IdAppointmentTimeNavigation)
                .LoadAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _appointments.Entry(appointment)
                .State = EntityState.Modified;

            await _hospitalContext.SaveChangesAsync();
        }
    }
}