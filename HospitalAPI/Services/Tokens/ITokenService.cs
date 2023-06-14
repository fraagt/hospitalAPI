using HospitalAPI.Database;

namespace HospitalAPI.Services.Tokens
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}