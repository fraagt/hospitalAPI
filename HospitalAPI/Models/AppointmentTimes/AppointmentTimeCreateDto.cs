namespace HospitalAPI.Models.AppointmentTimes
{
    public class AppointmentTimeCreateDto
    {
        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}