using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Accounts;

namespace HospitalAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<User, AuthenticationDto>();
            CreateMap<LoginDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<RegisterPatientDto, Patient>();
            CreateMap<RegisterDoctorDto, Doctor>();
        }
    }
}