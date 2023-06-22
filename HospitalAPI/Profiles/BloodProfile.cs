using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Blood;

namespace HospitalAPI.Profiles
{
    public class BloodProfile : Profile
    {
        public BloodProfile()
        {
            CreateMap<Blood, BloodReadDto>();
        }
    }
}