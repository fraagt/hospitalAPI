using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.WorkHistories;

namespace HospitalAPI.Profiles
{
    public class WorkHistoryProfile : Profile
    {
        public WorkHistoryProfile()
        {
            CreateMap<WorkHistory, WorkHistoryReadDto>();
            CreateMap<WorkHistoryCreateDto, WorkHistory>();
        }
    }
}