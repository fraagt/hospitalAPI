using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Users;
using HospitalAPI.Services.Accounts;
using HospitalAPI.Services.Tokens;
using HospitalAPI.Utils.Roles;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AdminController(
            IAccountService accountService,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationDto>> Register(RegisterUserDto registerAdmin)
        {
            if (await _accountService.GetUserByLogin(registerAdmin.Login) != null)
                return BadRequest("The login is already taken");
            if (await _accountService.GetUserByEmail(registerAdmin.Email) != null)
                return BadRequest("The email is already taken");

            var user = _mapper.Map<User>(registerAdmin);
            user.IdRole = EUserRole.Administrator.ToRoleId();
            
            await _accountService.RegisterUser(user, registerAdmin.Password);
            
            var authenticationDto = _mapper.Map<AuthenticationDto>(user);
            authenticationDto.Token = await _tokenService.CreateTokenAsync(user);

            return Ok(authenticationDto);
        }
    }
}