namespace HospitalAPI.Models.Blood
{
    public class BloodCreateDto
    {
        public string Typename { get; set; } = null!;

        public bool RhFactor { get; set; }
    }
}