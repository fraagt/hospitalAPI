namespace HospitalAPI.Database
{
    public partial class AppointmentTime
    {
        public int IdAppointmentTime { get; set; }

        public int IdDoctor { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool Reserved { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public virtual Doctor IdDoctorNavigation { get; set; } = null!;
    }
}
