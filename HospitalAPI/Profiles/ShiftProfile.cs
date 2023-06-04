using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Shifts;

namespace HospitalAPI.Profiles
{
    public class ShiftProfile : Profile
    {
        public ShiftProfile()
        {
            CreateMap<Shift, ShiftReadDto>();
            CreateMap<ShiftReadDto, Shift>();
        }
    }
}