using HospitalAPI.Models.Allergens;

namespace HospitalAPI.Models.Allergies
{
    public class AllergyReadDto
    {
        public int IdAllergy { get; set; }

        public AllergenReadDto Allergen { get; set; } = null!;

        public int IdMedicalCard { get; set; }

        public sbyte SeverityLevel { get; set; }

        public DateTime DiagnosisDate { get; set; }
    }
}