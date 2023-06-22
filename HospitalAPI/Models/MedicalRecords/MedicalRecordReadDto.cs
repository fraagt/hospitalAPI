using System.Collections;
using HospitalAPI.Models.Attachments;
using HospitalAPI.Models.Diagnoses;
using HospitalAPI.Models.Prescriptions;

namespace HospitalAPI.Models.MedicalRecords
{
    public class MedicalRecordReadDto
    {
        public int IdMedicalRecord { get; set; }

        public int IdMedicalCard { get; set; }

        public int IdDoctor { get; set; }

        public string Note { get; set; } = null!;

        public IEnumerable<PrescriptionReadDto> Prescriptions { get; set; } = null!;

        public IEnumerable<DiagnosisReadDto> Diagnoses { get; set; } = null!;

        public IEnumerable<AttachmentReadDto> Attachments { get; set; } = null!;
    }
}