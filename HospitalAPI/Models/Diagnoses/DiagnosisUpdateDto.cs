namespace HospitalAPI.Models.Diagnoses
{
    public class DiagnosisUpdateDto
    {
        public DateTime DiagnosedDate { get; set; }

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}