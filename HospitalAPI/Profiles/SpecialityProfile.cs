using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Specialities;

namespace HospitalAPI.Profiles
{
    public class SpecialityProfile : Profile
    {
        public SpecialityProfile()
        {
            CreateMap<Speciality, SpecialityReadDto>();
            CreateMap<SpecialityCreateDto, Speciality>();
        }
    }
}