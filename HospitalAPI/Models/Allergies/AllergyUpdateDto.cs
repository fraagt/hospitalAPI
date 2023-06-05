namespace HospitalAPI.Models.Allergies
{
    public class AllergyUpdateDto
    {
        public int IdAllergen { get; set; }

        public sbyte SeverityLevel { get; set; }

        public DateTime DiagnosisDate { get; set; }
    }
}