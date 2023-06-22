using HospitalAPI.Models.ContactInfos;
using HospitalAPI.Models.Genders;

namespace HospitalAPI.Models.Patients
{
    public class PatientReadDto
    {
        public int IdPatient { get; set; }

        public int IdUser { get; set; }

        public int IdMedicalCard { get; set; }

        public GenderReadDto? Gender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public IEnumerable<ContactInfoReadDto> ContactInfos { get; set; } = null!;
    }
}