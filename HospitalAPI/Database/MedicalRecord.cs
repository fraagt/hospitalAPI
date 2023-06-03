namespace HospitalAPI.Database
{
    public partial class MedicalRecord
    {
        public int IdMedicalRecord { get; set; }

        public int IdMedicalCard { get; set; }

        public int IdDoctor { get; set; }

        public string Note { get; set; } = null!;

        public bool HasAttachments { get; set; }

        public bool HasPrescriptions { get; set; }

        public bool HasDiagnosis { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

        public virtual Doctor IdDoctorNavigation { get; set; } = null!;

        public virtual MedicalCard IdMedicalCardNavigation { get; set; } = null!;

        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
