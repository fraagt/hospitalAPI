using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Users;

namespace HospitalAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AuthenticationDto>();
            CreateMap<LoginDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<RegisterPatientDto, Patient>();
            CreateMap<RegisterDoctorDto, Doctor>();
        }
    }
}