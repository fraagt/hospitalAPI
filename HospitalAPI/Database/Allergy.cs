namespace HospitalAPI.Database
{
    public partial class Allergy
    {
        public int IdAllergy { get; set; }

        public int IdAllergen { get; set; }

        public int IdMedicalCard { get; set; }

        public sbyte SeverityLevel { get; set; }

        public DateTime DiagnosisDate { get; set; }

        public virtual Allergen IdAllergenNavigation { get; set; } = null!;

        public virtual MedicalCard IdMedicalCardNavigation { get; set; } = null!;
    }
}
