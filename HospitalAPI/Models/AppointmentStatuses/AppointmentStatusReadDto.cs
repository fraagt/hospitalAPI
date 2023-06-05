namespace HospitalAPI.Models.AppointmentStatuses
{
    public class AppointmentStatusReadDto
    {
        public int IdAppointmentStatus { get; set; }

        public string Title { get; set; } = null!;
    }
}