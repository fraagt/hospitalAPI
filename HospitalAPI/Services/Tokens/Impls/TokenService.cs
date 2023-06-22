using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalAPI.Database;
using HospitalAPI.Repositories.Users;
using HospitalAPI.Utils.Identity;
using HospitalAPI.Utils.Roles;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.IdentityModel.Tokens;

namespace HospitalAPI.Services.Tokens.Impls
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly SymmetricSecurityKey _key;

        public TokenService(
            IUserRepository userRepository,
            IConfiguration config
        )
        {
            _userRepository = userRepository;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var userRole = RolesExtension.ToUserRole(user.IdRole);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Login),
                new(ClaimTypes.Role, userRole.ToString()),
                new(ClaimType.IdRole, user.IdRole.ToString())
            };

            int? idProfile = null;
            switch (userRole)
            {
                case EUserRole.Doctor:
                    // await _userRepository.LoadDoctorAsync(user);
                    idProfile = user.Doctors.First().IdDoctor;
                    claims.Add(new Claim(ClaimType.IdDoctor, idProfile.ToString()!));
                    break;
                case EUserRole.Patient:
                    await _userRepository.LoadPatientAsync(user);
                    idProfile = user.Patients.First().IdPatient;
                    claims.Add(new Claim(ClaimType.IdPatient, user.Patients.First().IdPatient.ToString()));
                    break;
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}