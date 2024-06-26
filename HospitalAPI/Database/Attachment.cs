﻿namespace HospitalAPI.Database
{
    public partial class Attachment
    {
        public int IdAttachment { get; set; }

        public int IdMedicalRecord { get; set; }

        public string FileName { get; set; } = null!;

        public string FileType { get; set; } = null!;

        public byte[] FileBytes { get; set; } = null!;

        public virtual MedicalRecord IdMedicalRecordNavigation { get; set; } = null!;
    }
}
