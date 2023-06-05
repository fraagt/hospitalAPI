namespace HospitalAPI.Models.Allergens
{
    public class AllergenReadDto
    {
        public int IdAllergen { get; set; }

        public string Title { get; set; } = null!;

        public string CategoryName { get; set; } = null!;
    }
}