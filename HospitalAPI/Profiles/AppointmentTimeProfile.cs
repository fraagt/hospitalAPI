using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentTimes;

namespace HospitalAPI.Profiles
{
    public class AppointmentTimeProfile : Profile
    {
        public AppointmentTimeProfile()
        {
            CreateMap<AppointmentTime, AppointmentTimeReadDto>();
            CreateMap<AppointmentTimeCreateDto, AppointmentTime>();
        }
    }
}