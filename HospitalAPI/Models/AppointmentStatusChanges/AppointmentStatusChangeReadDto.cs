namespace HospitalAPI.Models.AppointmentStatusChanges
{
    public class AppointmentStatusChangeReadDto
    {
        public int IdAppointmentStatusChange { get; set; }

        public int IdAppointment { get; set; }

        public int IdAppointmentStatus { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}