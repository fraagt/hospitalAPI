namespace HospitalAPI.Models.Attachments
{
    public class AttachmentCreateDto
    {
        public int IdMedicalRecord { get; set; }

        public string FileName { get; set; } = null!;

        public string FileType { get; set; } = null!;

        public byte[] FileBytes { get; set; } = null!;
    }
}