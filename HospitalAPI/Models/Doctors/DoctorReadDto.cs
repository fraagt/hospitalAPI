using HospitalAPI.Models.AppointmentTimes;
using HospitalAPI.Models.ContactInfos;
using HospitalAPI.Models.Genders;
using HospitalAPI.Models.Services;
using HospitalAPI.Models.Specialities;
using HospitalAPI.Models.WorkHistories;

namespace HospitalAPI.Models.Doctors
{
    public class DoctorReadDto
    {
        public int IdDoctor { get; set; }

        public int IdUser { get; set; }

        public GenderReadDto? Gender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Biography { get; set; } = null!;

        public IEnumerable<SpecialityReadDto> Specialities { get; set; } = null!;

        public IEnumerable<WorkHistoryReadDto> WorkHistories { get; set; } = null!;

        public IEnumerable<ContactInfoReadDto> ContactInfos { get; set; } = null!;

        public IEnumerable<ServiceReadDto> Services { get; set; } = null!;
        
        public IEnumerable<AppointmentTimeReadDto> AppointmentTimes { get; set; } = null!;
    }
}