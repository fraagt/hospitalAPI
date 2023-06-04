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
    }
}