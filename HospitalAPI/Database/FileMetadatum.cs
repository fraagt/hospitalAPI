namespace HospitalAPI.Database
{
    public partial class FileMetadatum
    {
        public int IdFileMetadata { get; set; }

        public int IdFileData { get; set; }

        public string FileName { get; set; } = null!;

        public string FileType { get; set; } = null!;

        public int SizeByte { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        public virtual FileDatum IdFileDataNavigation { get; set; } = null!;
    }
}
