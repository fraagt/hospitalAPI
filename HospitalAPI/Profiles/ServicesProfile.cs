using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Services;

namespace HospitalAPI.Profiles
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<Service, ServiceReadDto>();
            CreateMap<ServiceCreateDto, Service>();
        }
    }
}