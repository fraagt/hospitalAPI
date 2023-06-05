namespace HospitalAPI.Models.Allergies
{
    public class AllergyReadDto
    {
        public int IdAllergy { get; set; }

        public int IdAllergen { get; set; }

        public int IdMedicalCard { get; set; }

        public sbyte SeverityLevel { get; set; }

        public DateTime DiagnosisDate { get; set; }
    }
}