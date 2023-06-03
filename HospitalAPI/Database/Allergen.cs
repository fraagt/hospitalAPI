namespace HospitalAPI.Database
{
    public partial class Allergen
    {
        public int IdAllergen { get; set; }

        public string Title { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    }
}