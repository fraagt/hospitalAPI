namespace HospitalAPI.Models.Doctors
{
    public class DoctorReadDto
    {
        public int IdDoctor { get; set; }

        public int IdUser { get; set; }

        public int? IdGender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Biography { get; set; } = null!;
    }
}