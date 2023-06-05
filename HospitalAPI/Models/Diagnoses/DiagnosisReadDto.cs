namespace HospitalAPI.Models.Diagnoses
{
    public class DiagnosisReadDto
    {
        public int IdDiagnosis { get; set; }

        public int IdMedicalRecord { get; set; }

        public DateTime DiagnosedDate { get; set; }

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}