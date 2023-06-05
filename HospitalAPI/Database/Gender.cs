namespace HospitalAPI.Database
{
    public partial class Gender
    {
        public int IdGender { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
