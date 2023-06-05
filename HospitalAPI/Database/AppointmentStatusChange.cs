namespace HospitalAPI.Database
{
    public partial class AppointmentStatusChange
    {
        public int IdAppointmentStatusChange { get; set; }

        public int IdAppointment { get; set; }

        public int IdAppointmentStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Appointment IdAppointmentNavigation { get; set; } = null!;

        public virtual AppointmentStatus IdAppointmentStatusNavigation { get; set; } = null!;
    }
}
