using HospitalAPI.Models.Allergies;
using HospitalAPI.Models.Blood;
using HospitalAPI.Models.MedicalRecords;

namespace HospitalAPI.Models.MedicalCards
{
    public class MedicalCardReadDto
    {
        public int IdMedicalCard { get; set; }

        public int IdPatient { get; set; }

        public BloodReadDto? Blood { get; set; }

        public IEnumerable<AllergyReadDto> Allergies { get; set; } = null!;

        public IEnumerable<MedicalRecordReadDto> MedicalRecords { get; set; } = null!;
    }
}