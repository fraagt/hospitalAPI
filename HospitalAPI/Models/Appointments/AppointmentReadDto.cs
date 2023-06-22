using System.Collections;
using HospitalAPI.Models.AppointmentStatusChanges;
using HospitalAPI.Models.AppointmentStatuses;
using HospitalAPI.Models.AppointmentTimes;
using HospitalAPI.Models.Services;

namespace HospitalAPI.Models.Appointments
{
    public class AppointmentReadDto
    {
        public int IdAppointment { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }

        public AppointmentTimeReadDto AppointmentTime { get; set; } = null!;

        public ServiceReadDto Service { get; set; } = null!;

        public AppointmentStatusReadDto CurrentStatus { get; set; } = null!;

        public IEnumerable<AppointmentStatusChangeReadDto> StatusChanges { get; set; } = null!;
    }
}