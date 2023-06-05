namespace HospitalAPI.Models.AppointmentStatusChanges
{
    public class AppointmentStatusChangeCreateDto
    {
        public int IdAppointment { get; set; }

        public int IdAppointmentStatus { get; set; }
    }
}