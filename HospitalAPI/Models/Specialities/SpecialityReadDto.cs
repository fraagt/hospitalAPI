namespace HospitalAPI.Models.Specialities
{
    public class SpecialityReadDto
    {
        public int IdSpeciality { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}