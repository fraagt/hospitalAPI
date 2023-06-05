using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Allergies;

namespace HospitalAPI.Profiles
{
    public class AllergyProfile : Profile
    {
        public AllergyProfile()
        {
            CreateMap<Allergy, AllergyReadDto>();
            CreateMap<AllergyCreateDto, Allergy>();
            CreateMap<AllergyUpdateDto, Allergy>();
        }
    }
}