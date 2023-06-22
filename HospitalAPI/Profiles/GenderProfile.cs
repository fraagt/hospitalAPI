using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Genders;

namespace HospitalAPI.Profiles
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, GenderReadDto>();
        }
    }
}