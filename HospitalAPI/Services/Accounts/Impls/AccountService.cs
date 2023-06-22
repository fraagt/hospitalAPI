using System.Security.Cryptography;
using System.Text;
using HospitalAPI.Database;
using HospitalAPI.Repositories.Genders;
using HospitalAPI.Repositories.Users;

namespace HospitalAPI.Services.Accounts.Impls
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;

        public AccountService(
            IUserRepository userRepository,
            IGenderRepository genderRepository
        )
        {
            _userRepository = userRepository;
            _genderRepository = genderRepository;
        }

        public async Task<User?> GetUserByLogin(string login)
        {
            return await _userRepository.GetByLoginAsync(login);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
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

        public async Task<IEnumerable<Gender>> GetGenders()
        {
            return await _genderRepository.GetAsync();
        }
    }
}