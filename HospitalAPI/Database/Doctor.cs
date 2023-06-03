namespace HospitalAPI.Database
{
    public partial class Doctor
    {
        public int IdDoctor { get; set; }

        public int IdUser { get; set; }

        public int? IdGender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Biography { get; set; } = null!;

        public virtual ICollection<AppointmentTime> AppointmentTimes { get; set; } = new List<AppointmentTime>();

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public virtual Gender? IdGenderNavigation { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;

        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

        public virtual ICollection<WorkHistory> WorkHistories { get; set; } = new List<WorkHistory>();

        public virtual ICollection<ContactInfo> IdContactInfos { get; set; } = new List<ContactInfo>();

        public virtual ICollection<Service> IdServices { get; set; } = new List<Service>();

        public virtual ICollection<Speciality> IdSpecialities { get; set; } = new List<Speciality>();
    }
}
