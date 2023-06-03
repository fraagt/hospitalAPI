namespace HospitalAPI.Database
{
    public partial class Service
    {
        public int IdService { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public virtual ICollection<Doctor> IdDoctors { get; set; } = new List<Doctor>();
    }
}
