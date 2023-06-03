namespace HospitalAPI.Database
{
    public partial class Shift
    {
        public int IdShift { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int IdDoctor { get; set; }

        public virtual Doctor IdDoctorNavigation { get; set; } = null!;
    }
}
