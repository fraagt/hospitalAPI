using HospitalAPI.Database;
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

        public async Task<IEnumerable<Appointment>> GetAsync()
        {
            return await _appointments.ToListAsync();
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