using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Allergens;

namespace HospitalAPI.Profiles
{
    public class AllergenProfile : Profile
    {
        public AllergenProfile()
        {
            CreateMap<Allergen, AllergenReadDto>();
            CreateMap<AllergenCreateDto, Allergen>();
        }
    }
}