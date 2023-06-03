namespace HospitalAPI.Database
{
    public partial class FileDatum
    {
        public int IdFileData { get; set; }

        public byte[] FileBytes { get; set; } = null!;

        public virtual ICollection<FileMetadatum> FileMetadata { get; set; } = new List<FileMetadatum>();
    }
}
