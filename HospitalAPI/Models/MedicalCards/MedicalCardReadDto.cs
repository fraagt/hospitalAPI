namespace HospitalAPI.Models.MedicalCards
{
    public class MedicalCardReadDto
    {
        public int IdMedicalCard { get; set; }

        public int IdPatient { get; set; }

        public int? IdBlood { get; set; }
    }
}