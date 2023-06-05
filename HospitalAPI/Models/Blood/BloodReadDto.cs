namespace HospitalAPI.Models.Blood
{
    public class BloodReadDto
    {
        public int IdBlood { get; set; }

        public string Typename { get; set; } = null!;

        public bool RhFactor { get; set; }
    }
}