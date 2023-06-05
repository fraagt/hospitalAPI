using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.MedicalRecords;

namespace HospitalAPI.Profiles
{
    public class MedicalRecordProfile : Profile
    {
        public MedicalRecordProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordReadDto>();
            CreateMap<MedicalRecordCreateDto, MedicalRecord>();
            CreateMap<MedicalRecordUpdateDto, MedicalRecord>();
        }
    }
}