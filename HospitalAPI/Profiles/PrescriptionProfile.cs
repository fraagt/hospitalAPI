using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Prescriptions;

namespace HospitalAPI.Profiles
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<Prescription, PrescriptionReadDto>();
            CreateMap<PrescriptionCreateDto, Prescription>();
            CreateMap<PrescriptionUpdateDto, Prescription>();
        }
    }
}