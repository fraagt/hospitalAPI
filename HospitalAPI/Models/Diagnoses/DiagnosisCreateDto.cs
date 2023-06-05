namespace HospitalAPI.Models.Diagnoses
{
    public class DiagnosisCreateDto
    {
        public int IdMedicalRecord { get; set; }

        public DateTime DiagnosedDate { get; set; }

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}