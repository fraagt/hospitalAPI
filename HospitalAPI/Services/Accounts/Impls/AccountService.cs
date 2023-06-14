using System.Security.Cryptography;
using System.Text;
using HospitalAPI.Database;
using HospitalAPI.Repositories.Users;

namespace HospitalAPI.Services.Accounts.Impls
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByLogin(string login)
        {
            return await _userRepository.GetByLoginAsync(login);
        }

        public bool CheckPassword(User user, string password)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return !computedHash.Where((t, i) => t != user.PasswordHash[i]).Any();
        }

        public async Task RegisterUser(User user, string password)
        {
            using var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;

            await _userRepository.CreateAsync(user);
        }

        public async Task<bool> IsUserExist(string login)
        {
            return await GetUserByLogin(login) != null;
        }
    }
}