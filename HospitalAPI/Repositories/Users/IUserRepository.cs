using HospitalAPI.Database;

namespace HospitalAPI.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByLoginAsync(string login);
        Task<User?> GetByEmailAsync(string email);
        Task LoadDoctorAsync(User user);
        Task LoadPatientAsync(User user);
        Task CreateAsync(User user);
    }
}