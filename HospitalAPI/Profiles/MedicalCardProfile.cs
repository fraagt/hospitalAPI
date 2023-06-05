using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.MedicalCards;

namespace HospitalAPI.Profiles
{
    public class MedicalCardProfile : Profile
    {
        public MedicalCardProfile()
        {
            CreateMap<MedicalCard, MedicalCardReadDto>();
            CreateMap<MedicalCardUpdateDto, MedicalCard>();
        }
    }
}