using HospitalAPI.Database;

namespace HospitalAPI.Services.Accounts
{
    public interface IAccountService
    {
        Task<User?> GetUserByLogin(string login);
        Task<User?> GetUserByEmail(string email);
        bool CheckPassword(User user, string password);
        Task RegisterUser(User user, string password);
        Task<IEnumerable<Gender>> GetGenders();
    }
}