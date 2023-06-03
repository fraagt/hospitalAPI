namespace HospitalAPI.Database
{
    public partial class Patient
    {
        public int IdPatient { get; set; }

        public int IdUser { get; set; }

        public int? IdGender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public virtual Gender? IdGenderNavigation { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;

        public virtual ICollection<MedicalCard> MedicalCards { get; set; } = new List<MedicalCard>();

        public virtual ICollection<ContactInfo> IdContactInfos { get; set; } = new List<ContactInfo>();
    }
}
