namespace HospitalAPI.Database
{
    public partial class Attachment
    {
        public int IdAttachment { get; set; }

        public int IdMedicalRecord { get; set; }

        public int IdFileMetadata { get; set; }

        public int IdFileData { get; set; }

        public virtual FileMetadatum IdFile { get; set; } = null!;

        public virtual MedicalRecord IdMedicalRecordNavigation { get; set; } = null!;
    }
}