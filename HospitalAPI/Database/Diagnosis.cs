namespace HospitalAPI.Database
{
    public partial class Diagnosis
    {
        public int IdDiagnosis { get; set; }

        public int IdMedicalRecord { get; set; }

        public DateTime DiagnosedDate { get; set; }

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual MedicalRecord IdMedicalRecordNavigation { get; set; } = null!;
    }
}
