namespace HospitalAPI.Database
{
    public partial class Speciality
    {
        public int IdSpeciality { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<Doctor> IdDoctors { get; set; } = new List<Doctor>();
    }
}
