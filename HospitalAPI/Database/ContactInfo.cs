namespace HospitalAPI.Database
{
    public partial class ContactInfo
    {
        public int IdContactInfo { get; set; }

        public string Label { get; set; } = null!;

        public string ValueType { get; set; } = null!;

        public string ContactValue { get; set; } = null!;

        public virtual ICollection<Doctor> IdDoctors { get; set; } = new List<Doctor>();

        public virtual ICollection<Patient> IdPatients { get; set; } = new List<Patient>();
    }
}
