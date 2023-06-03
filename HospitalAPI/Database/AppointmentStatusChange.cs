namespace HospitalAPI.Database
{
    public partial class AppointmentStatusChange
    {
        public int IdAppointmentStatusChange { get; set; }

        public int IdAppointment { get; set; }

        public int IsAppointmentStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Appointment IdAppointmentNavigation { get; set; } = null!;

        public virtual AppointmentStatus IsAppointmentStatusNavigation { get; set; } = null!;
    }
}