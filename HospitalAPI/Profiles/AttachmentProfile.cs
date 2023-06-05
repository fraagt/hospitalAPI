using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Attachments;

namespace HospitalAPI.Profiles
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentReadDto>();
            CreateMap<AttachmentCreateDto, Attachment>();
            CreateMap<AttachmentUpdateDto, Attachment>();
        }
    }
}