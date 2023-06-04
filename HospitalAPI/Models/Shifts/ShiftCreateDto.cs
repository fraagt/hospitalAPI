namespace HospitalAPI.Models.Shifts
{
    public class ShiftCreateDto
    {
        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}