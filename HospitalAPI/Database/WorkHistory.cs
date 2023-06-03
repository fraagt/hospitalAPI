namespace HospitalAPI.Database
{
    public partial class WorkHistory
    {
        public int IdWorkHistory { get; set; }

        public int IdDoctor { get; set; }

        public string Title { get; set; } = null!;

        public string Organization { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual Doctor IdDoctorNavigation { get; set; } = null!;
    }
}
