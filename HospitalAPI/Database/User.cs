namespace HospitalAPI.Database
{
    public partial class User
    {
        public int IdUser { get; set; }

        public int IdRole { get; set; }

        public string Login { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public virtual Role IdRoleNavigation { get; set; } = null!;

        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
