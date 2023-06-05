namespace HospitalAPI.Models.MedicalRecords
{
    public class MedicalRecordCreateDto
    {
        public int IdMedicalCard { get; set; }

        public string Note { get; set; } = null!;
    }
}