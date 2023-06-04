using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.ContactInfos;

namespace HospitalAPI.Profiles
{
    public class ContactInfoProfile : Profile
    {
        public ContactInfoProfile()
        {
            CreateMap<ContactInfo, ContactInfoReadDto>();
            CreateMap<ContactInfoCreateDto, ContactInfo>();
        }
    }
}