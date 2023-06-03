namespace HospitalAPI.Database
{
    public partial class MedicalCard
    {
        public int IdMedicalCard { get; set; }

        public int IdPatient { get; set; }

        public int? IdBlood { get; set; }

        public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

        public virtual Blood? IdBloodNavigation { get; set; }

        public virtual Patient IdPatientNavigation { get; set; } = null!;

        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
