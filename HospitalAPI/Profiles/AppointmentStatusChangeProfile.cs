using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentStatusChanges;

namespace HospitalAPI.Profiles
{
    public class AppointmentStatusChangeProfile : Profile
    {
        public AppointmentStatusChangeProfile()
        {
            CreateMap<AppointmentStatusChange, AppointmentStatusChangeReadDto>();
            CreateMap<AppointmentStatusChangeCreateDto, AppointmentStatusChange>();
        }
    }
}