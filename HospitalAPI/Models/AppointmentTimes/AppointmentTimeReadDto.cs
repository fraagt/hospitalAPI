namespace HospitalAPI.Models.AppointmentTimes
{
    public class AppointmentTimeReadDto
    {
        public int IdAppointmentTime { get; set; }

        public int IdDoctor { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool Reserved { get; set; }
    }
}