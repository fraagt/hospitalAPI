using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Diagnoses;

namespace HospitalAPI.Profiles
{
    public class DiagnosisProfile : Profile
    {
        public DiagnosisProfile()
        {
            CreateMap<Diagnosis, DiagnosisReadDto>();
            CreateMap<DiagnosisCreateDto, Diagnosis>();
            CreateMap<DiagnosisUpdateDto, Diagnosis>();
        }
    }
}