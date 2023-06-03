﻿namespace HospitalAPI.Database
{
    public partial class AppointmentStatus
    {
        public int IsAppointmentStatus { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<AppointmentStatusChange> AppointmentStatusChanges { get; set; } = new List<AppointmentStatusChange>();
    }
}
