namespace HospitalAPI.Database
{
    public partial class Appointment
    {
        public int IdAppointment { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }

        public int IdAppointmentTime { get; set; }

        public int IdService { get; set; }

        public virtual ICollection<AppointmentStatusChange> AppointmentStatusChanges { get; set; } = new List<AppointmentStatusChange>();

        public virtual AppointmentTime IdAppointmentTimeNavigation { get; set; } = null!;

        public virtual Doctor IdDoctorNavigation { get; set; } = null!;

        public virtual Patient IdPatientNavigation { get; set; } = null!;

        public virtual Service IdServiceNavigation { get; set; } = null!;
    }
}
