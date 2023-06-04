namespace HospitalAPI.Models.Appointments
{
    public class AppointmentReadDto
    {
        public int IdAppointment { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }

        public int IdAppointmentTime { get; set; }

        public int IdService { get; set; }
    }
}