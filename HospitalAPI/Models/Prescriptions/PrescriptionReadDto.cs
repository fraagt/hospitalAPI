﻿namespace HospitalAPI.Models.Prescriptions
{
    public class PrescriptionReadDto
    {
        public int IdPrescription { get; set; }

        public int IdMedicalRecord { get; set; }

        public string MedicineName { get; set; } = null!;

        public string MedicineStrength { get; set; } = null!;

        public string RouteOfAdministration { get; set; } = null!;

        public int RefillsCount { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string AdverseReactions { get; set; } = null!;

        public string StorageRequirements { get; set; } = null!;
    }
}