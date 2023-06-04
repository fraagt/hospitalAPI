namespace HospitalAPI.Models.Patients
{
    public class PatientUpdateDto
    {
        public int? IdGender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }
    }
}