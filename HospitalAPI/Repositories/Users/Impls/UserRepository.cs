using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.Users.Impls
{
    public class UserRepository : IUserRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<User> _users;

        public UserRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _users = hospitalContext.Users;
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _users.FirstOrDefaultAsync(user => user.Login.Equals(login));
        }

        public async Task LoadDoctorAsync(User user)
        {
            await _users.Entry(user)
                .Collection(u => u.Doctors)
                .LoadAsync();
        }

        public async Task LoadPatientAsync(User user)
        {
            await _users.Entry(user)
                .Collection(u => u.Patients)
                .LoadAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.AddAsync(user);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}