namespace HospitalAPI.Models.Shifts
{
    public class ShiftReadDto
    {
        public int IdShift { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int IdDoctor { get; set; }
    }
}