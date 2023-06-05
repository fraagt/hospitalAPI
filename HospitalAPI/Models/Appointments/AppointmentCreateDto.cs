namespace HospitalAPI.Models.Appointments
{
    public class AppointmentCreateDto
    {
        public int IdDoctor { get; set; }

        public int IdAppointmentTime { get; set; }

        public int IdService { get; set; }
    }
}