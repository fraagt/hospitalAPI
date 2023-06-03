using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Doctors;

namespace HospitalAPI.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorReadDto>();
            CreateMap<DoctorUpdateDto, Doctor>();
        }
    }
}