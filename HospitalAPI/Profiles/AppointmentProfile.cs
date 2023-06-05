using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Appointments;

namespace HospitalAPI.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentReadDto>();
            CreateMap<AppointmentCreateDto, Appointment>();
        }
    }
}