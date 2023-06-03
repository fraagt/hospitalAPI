namespace HospitalAPI.Models.WorkHistories
{
    public class WorkHistoryCreateDto
    {
        public string Title { get; set; } = null!;

        public string Organization { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}