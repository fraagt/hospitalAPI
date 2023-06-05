namespace HospitalAPI.Models.MedicalRecords
{
    public class MedicalRecordReadDto
    {
        public int IdMedicalRecord { get; set; }

        public int IdMedicalCard { get; set; }

        public int IdDoctor { get; set; }

        public string Note { get; set; } = null!;

        public bool HasAttachments { get; set; }

        public bool HasPrescriptions { get; set; }

        public bool HasDiagnoses { get; set; }
    }
}