namespace HospitalAPI.Models.Attachments
{
    public class AttachmentUpdateDto
    {
        public string FileName { get; set; } = null!;

        public string FileType { get; set; } = null!;

        public byte[] FileBytes { get; set; } = null!;
    }
}