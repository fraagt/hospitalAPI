using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Patients;

namespace HospitalAPI.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientReadDto>();
            CreateMap<PatientUpdateDto, Patient>();
        }
    }
}