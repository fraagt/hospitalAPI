namespace HospitalAPI.Database
{
    public partial class Blood
    {
        public int IdBlood { get; set; }

        public string Typename { get; set; } = null!;

        public bool RhFactor { get; set; }

        public virtual ICollection<MedicalCard> MedicalCards { get; set; } = new List<MedicalCard>();
    }
}