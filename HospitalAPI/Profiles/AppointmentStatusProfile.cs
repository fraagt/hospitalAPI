using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentStatuses;

namespace HospitalAPI.Profiles
{
    public class AppointmentStatusProfile : Profile
    {
        public AppointmentStatusProfile()
        {
            CreateMap<AppointmentStatus, AppointmentStatusReadDto>();
        }
    }
}