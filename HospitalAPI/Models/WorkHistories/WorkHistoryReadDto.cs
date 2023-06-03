namespace HospitalAPI.Models.WorkHistories
{
    public class WorkHistoryReadDto
    {
        public int IdWorkHistory { get; set; }

        public int IdDoctor { get; set; }

        public string Title { get; set; } = null!;

        public string Organization { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}