namespace HospitalAPI.Models.Doctors
{
    public class GetDoctorsFilters
    {
        public int? IdSpeciality { get; set; }

        public DateTime? AppointmentAvailableDate { get; set; }
    }
}