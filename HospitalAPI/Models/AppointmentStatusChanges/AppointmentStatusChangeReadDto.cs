using HospitalAPI.Models.AppointmentStatuses;

namespace HospitalAPI.Models.AppointmentStatusChanges
{
    public class AppointmentStatusChangeReadDto
    {
        public int IdAppointmentStatusChange { get; set; }

        public int IdAppointment { get; set; }

        public AppointmentStatusReadDto AppointmentStatus { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}