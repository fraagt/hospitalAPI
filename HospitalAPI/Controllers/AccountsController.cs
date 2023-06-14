using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Accounts;
using HospitalAPI.Services.Accounts;
using HospitalAPI.Services.Doctors;
using HospitalAPI.Services.Patients;
using HospitalAPI.Services.Tokens;
using HospitalAPI.Utils.Roles;
using HospitalAPI.Utils.Roles.Attributes;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IPatientsService _patientsService;
        private readonly IDoctorsService _doctorsService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(
            IAccountService accountService,
            IPatientsService patientsService,
            IDoctorsService doctorsService,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            _accountService = accountService;
            _patientsService = patientsService;
            _doctorsService = doctorsService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDto>> Login(LoginDto loginDto)
        {
            var user = await _accountService.GetUserByLogin(loginDto.Login);
            if (user == null)
            {
                return Unauthorized("Bad credentials");
            }

            if (!_accountService.CheckPassword(user, loginDto.Password))
                return Unauthorized("Bad credentials");
            
            var loginResponseDto = _mapper.Map<AuthenticationDto>(user);
            loginResponseDto.Token = await _tokenService.CreateTokenAsync(user);

            return Ok(loginResponseDto);
        }
        
        [AllowAnonymous]
        [HttpPost("registerPatient")]
        public async Task<ActionResult<AuthenticationDto>> RegisterPatient(RegisterPatientDto registerPatientDto)
        {
            if (await _accountService.IsUserExist(registerPatientDto.Login))
                return BadRequest("The login is already taken");

            var user = _mapper.Map<User>(registerPatientDto);
            user.IdRole = EUserRole.Patient.ToRoleId();
            
            await _accountService.RegisterUser(user, registerPatientDto.Password);

            var patient = _mapper.Map<Patient>(registerPatientDto);
            patient.IdUser = user.IdUser;
            await _patientsService.CreatePatient(patient);
            
            var authenticationDto = _mapper.Map<AuthenticationDto>(user);
            authenticationDto.Token = await _tokenService.CreateTokenAsync(user);

            return Ok(authenticationDto);
        }
        
        [RoleAuthorize(EUserRole.Administrator)]
        [HttpPost("registerDoctor")]
        public async Task<ActionResult<AuthenticationDto>> RegisterDoctor(RegisterDoctorDto registerDoctorDto)
        {
            if (await _accountService.IsUserExist(registerDoctorDto.Login))
                return BadRequest("The login is already taken");

            var user = _mapper.Map<User>(registerDoctorDto);
            user.IdRole = EUserRole.Patient.ToRoleId();
            
            await _accountService.RegisterUser(user, registerDoctorDto.Password);

            var doctor = _mapper.Map<Doctor>(registerDoctorDto);
            doctor.IdUser = user.IdUser;
            await _doctorsService.CreateDoctor(doctor);
            
            var authenticationDto = _mapper.Map<AuthenticationDto>(user);
            authenticationDto.Token = await _tokenService.CreateTokenAsync(user);

            return Ok(authenticationDto);
        }

    }
}