namespace HospitalAPI.Models.Patients
{
    public class PatientReadDto
    {
        public int IdPatient { get; set; }

        public int IdUser { get; set; }

        public int? IdGender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }
    }
}