using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Database
{
    public partial class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allergen> Allergens { get; set; }

        public virtual DbSet<Allergy> Allergies { get; set; }

        public virtual DbSet<Appointment> Appointments { get; set; }

        public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

        public virtual DbSet<AppointmentStatusChange> AppointmentStatusChanges { get; set; }

        public virtual DbSet<AppointmentTime> AppointmentTimes { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<Blood> Bloods { get; set; }

        public virtual DbSet<ContactInfo> ContactInfos { get; set; }

        public virtual DbSet<Diagnosis> Diagnoses { get; set; }

        public virtual DbSet<Doctor> Doctors { get; set; }

        public virtual DbSet<FileDatum> FileData { get; set; }

        public virtual DbSet<FileMetadatum> FileMetadata { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<MedicalCard> MedicalCards { get; set; }

        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<Prescription> Prescriptions { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<Shift> Shifts { get; set; }

        public virtual DbSet<Speciality> Specialities { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<WorkHistory> WorkHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=root;database=hospital");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allergen>(entity =>
            {
                entity.HasKey(e => e.IdAllergen).HasName("PRIMARY");

                entity.ToTable("allergen");

                entity.Property(e => e.IdAllergen).HasColumnName("idAllergen");
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.HasKey(e => new { e.IdAllergy, e.IdAllergen, e.IdMedicalCard }).HasName("PRIMARY");

                entity.ToTable("allergy");

                entity.HasIndex(e => e.IdAllergen, "R_44");

                entity.HasIndex(e => e.IdMedicalCard, "R_45");

                entity.Property(e => e.IdAllergy)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idAllergy");
                entity.Property(e => e.IdAllergen).HasColumnName("idAllergen");
                entity.Property(e => e.IdMedicalCard).HasColumnName("idMedicalCard");
                entity.Property(e => e.DiagnosisDate)
                    .HasColumnType("date")
                    .HasColumnName("diagnosis_date");
                entity.Property(e => e.SeverityLevel).HasColumnName("severity_level");

                entity.HasOne(d => d.IdAllergenNavigation).WithMany(p => p.Allergies)
                    .HasForeignKey(d => d.IdAllergen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("allergy_ibfk_1");

                entity.HasOne(d => d.IdMedicalCardNavigation).WithMany(p => p.Allergies)
                    .HasForeignKey(d => d.IdMedicalCard)
                    .HasConstraintName("allergy_ibfk_2");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.IdAppointment).HasName("PRIMARY");

                entity.ToTable("appointment");

                entity.HasIndex(e => e.IdPatient, "R_10343");

                entity.HasIndex(e => e.IdAppointmentTime, "R_35");

                entity.HasIndex(e => e.IdService, "R_36");

                entity.HasIndex(e => e.IdDoctor, "R_37");

                entity.Property(e => e.IdAppointment).HasColumnName("idAppointment");
                entity.Property(e => e.IdAppointmentTime).HasColumnName("idAppointmentTime");
                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
                entity.Property(e => e.IdPatient).HasColumnName("idPatient");
                entity.Property(e => e.IdService).HasColumnName("idService");

                entity.HasOne(d => d.IdAppointmentTimeNavigation).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.IdAppointmentTime)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_ibfk_3");

                entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_ibfk_2");

                entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.IdPatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_ibfk_1");

                entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.IdService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_ibfk_4");
            });

            modelBuilder.Entity<AppointmentStatus>(entity =>
            {
                entity.HasKey(e => e.IdAppointmentStatus).HasName("PRIMARY");

                entity.ToTable("appointment_status");

                entity.Property(e => e.IdAppointmentStatus).HasColumnName("idAppointmentStatus");
                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<AppointmentStatusChange>(entity =>
            {
                entity.HasKey(e => new { e.IdAppointmentStatusChange, e.IdAppointment, e.IdAppointmentStatus }).HasName("PRIMARY");

                entity.ToTable("appointment_status_change");

                entity.HasIndex(e => e.IdAppointment, "R_38");

                entity.HasIndex(e => e.IdAppointmentStatus, "R_39");

                entity.Property(e => e.IdAppointmentStatusChange)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idAppointmentStatusChange");
                entity.Property(e => e.IdAppointment).HasColumnName("idAppointment");
                entity.Property(e => e.IdAppointmentStatus).HasColumnName("idAppointmentStatus");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.HasOne(d => d.IdAppointmentNavigation).WithMany(p => p.AppointmentStatusChanges)
                    .HasForeignKey(d => d.IdAppointment)
                    .HasConstraintName("appointment_status_change_ibfk_1");

                entity.HasOne(d => d.IdAppointmentStatusNavigation).WithMany(p => p.AppointmentStatusChanges)
                    .HasForeignKey(d => d.IdAppointmentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_status_change_ibfk_2");
            });

            modelBuilder.Entity<AppointmentTime>(entity =>
            {
                entity.HasKey(e => e.IdAppointmentTime).HasName("PRIMARY");

                entity.ToTable("appointment_time");

                entity.HasIndex(e => e.IdDoctor, "R_31");

                entity.Property(e => e.IdAppointmentTime).HasColumnName("idAppointmentTime");
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");
                entity.Property(e => e.EndTime)
                    .HasColumnType("time")
                    .HasColumnName("end_time");
                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
                entity.Property(e => e.Reserved).HasColumnName("reserved");
                entity.Property(e => e.StartTime)
                    .HasColumnType("time")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.AppointmentTimes)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("appointment_time_ibfk_1");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => new { e.IdAttachment, e.IdFileMetadata, e.IdFileData, e.IdMedicalRecord }).HasName("PRIMARY");

                entity.ToTable("attachment");

                entity.HasIndex(e => new { e.IdFileMetadata, e.IdFileData }, "R_51");

                entity.HasIndex(e => e.IdMedicalRecord, "R_52");

                entity.Property(e => e.IdAttachment)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idAttachment");
                entity.Property(e => e.IdFileMetadata).HasColumnName("idFileMetadata");
                entity.Property(e => e.IdFileData).HasColumnName("idFileData");
                entity.Property(e => e.IdMedicalRecord).HasColumnName("idMedicalRecord");

                entity.HasOne(d => d.IdMedicalRecordNavigation).WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.IdMedicalRecord)
                    .HasConstraintName("attachment_ibfk_2");

                entity.HasOne(d => d.IdFile).WithMany(p => p.Attachments)
                    .HasForeignKey(d => new { d.IdFileMetadata, d.IdFileData })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attachment_ibfk_1");
            });

            modelBuilder.Entity<Blood>(entity =>
            {
                entity.HasKey(e => e.IdBlood).HasName("PRIMARY");

                entity.ToTable("blood");

                entity.Property(e => e.IdBlood).HasColumnName("idBlood");
                entity.Property(e => e.RhFactor).HasColumnName("rh_factor");
                entity.Property(e => e.Typename)
                    .HasMaxLength(3)
                    .HasColumnName("typename");
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasKey(e => e.IdContactInfo).HasName("PRIMARY");

                entity.ToTable("contact_info");

                entity.Property(e => e.IdContactInfo).HasColumnName("idContactInfo");
                entity.Property(e => e.ContactValue)
                    .HasMaxLength(255)
                    .HasColumnName("contact_value");
                entity.Property(e => e.Label)
                    .HasMaxLength(50)
                    .HasColumnName("label");
                entity.Property(e => e.ValueType)
                    .HasMaxLength(50)
                    .HasColumnName("value_type");
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasKey(e => new { e.IdDiagnosis, e.IdMedicalRecord }).HasName("PRIMARY");

                entity.ToTable("diagnosis");

                entity.HasIndex(e => e.IdMedicalRecord, "R_48");

                entity.Property(e => e.IdDiagnosis)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idDiagnosis");
                entity.Property(e => e.IdMedicalRecord).HasColumnName("idMedicalRecord");
                entity.Property(e => e.Code)
                    .HasMaxLength(15)
                    .HasColumnName("code");
                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.DiagnosedDate)
                    .HasColumnType("date")
                    .HasColumnName("diagnosed_date");

                entity.HasOne(d => d.IdMedicalRecordNavigation).WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.IdMedicalRecord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("diagnosis_ibfk_1");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("PRIMARY");

                entity.ToTable("doctor");

                entity.HasIndex(e => e.IdUser, "R_16");

                entity.HasIndex(e => e.IdGender, "R_43");

                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
                entity.Property(e => e.Biography)
                    .HasColumnType("text")
                    .HasColumnName("biography");
                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");
                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .HasColumnName("firstname");
                entity.Property(e => e.IdGender).HasColumnName("idGender");
                entity.Property(e => e.IdUser).HasColumnName("idUser");
                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .HasColumnName("lastname");

                entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.IdGender)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("doctor_ibfk_2");

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_ibfk_1");

                entity.HasMany(d => d.IdContactInfos).WithMany(p => p.IdDoctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DoctorContactInfo",
                        r => r.HasOne<ContactInfo>().WithMany()
                            .HasForeignKey("IdContactInfo")
                            .HasConstraintName("doctor_contact_info_ibfk_2"),
                        l => l.HasOne<Doctor>().WithMany()
                            .HasForeignKey("IdDoctor")
                            .OnDelete(DeleteBehavior.Restrict)
                            .HasConstraintName("doctor_contact_info_ibfk_1"),
                        j =>
                        {
                            j.HasKey("IdDoctor", "IdContactInfo").HasName("PRIMARY");
                            j.ToTable("doctor_contact_info");
                            j.HasIndex(new[] { "IdContactInfo" }, "R_22");
                            j.IndexerProperty<int>("IdDoctor").HasColumnName("idDoctor");
                            j.IndexerProperty<int>("IdContactInfo").HasColumnName("idContactInfo");
                        });

                entity.HasMany(d => d.IdServices).WithMany(p => p.IdDoctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DoctorService",
                        r => r.HasOne<Service>().WithMany()
                            .HasForeignKey("IdService")
                            .OnDelete(DeleteBehavior.Restrict)
                            .HasConstraintName("doctor_service_ibfk_2"),
                        l => l.HasOne<Doctor>().WithMany()
                            .HasForeignKey("IdDoctor")
                            .HasConstraintName("doctor_service_ibfk_1"),
                        j =>
                        {
                            j.HasKey("IdDoctor", "IdService").HasName("PRIMARY");
                            j.ToTable("doctor_service");
                            j.HasIndex(new[] { "IdService" }, "R_33");
                            j.IndexerProperty<int>("IdDoctor").HasColumnName("idDoctor");
                            j.IndexerProperty<int>("IdService").HasColumnName("idService");
                        });

                entity.HasMany(d => d.IdSpecialities).WithMany(p => p.IdDoctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DoctorSpeciality",
                        r => r.HasOne<Speciality>().WithMany()
                            .HasForeignKey("IdSpeciality")
                            .OnDelete(DeleteBehavior.Restrict)
                            .HasConstraintName("doctor_speciality_ibfk_2"),
                        l => l.HasOne<Doctor>().WithMany()
                            .HasForeignKey("IdDoctor")
                            .HasConstraintName("doctor_speciality_ibfk_1"),
                        j =>
                        {
                            j.HasKey("IdDoctor", "IdSpeciality").HasName("PRIMARY");
                            j.ToTable("doctor_speciality");
                            j.HasIndex(new[] { "IdSpeciality" }, "R_18");
                            j.IndexerProperty<int>("IdDoctor").HasColumnName("idDoctor");
                            j.IndexerProperty<int>("IdSpeciality").HasColumnName("idSpeciality");
                        });
            });

            modelBuilder.Entity<FileDatum>(entity =>
            {
                entity.HasKey(e => e.IdFileData).HasName("PRIMARY");

                entity.ToTable("file_data");

                entity.Property(e => e.IdFileData).HasColumnName("idFileData");
                entity.Property(e => e.FileBytes)
                    .HasColumnType("mediumblob")
                    .HasColumnName("file_bytes");
            });

            modelBuilder.Entity<FileMetadatum>(entity =>
            {
                entity.HasKey(e => new { e.IdFileMetadata, e.IdFileData }).HasName("PRIMARY");

                entity.ToTable("file_metadata");

                entity.HasIndex(e => e.IdFileData, "R_49");

                entity.Property(e => e.IdFileMetadata)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idFileMetadata");
                entity.Property(e => e.IdFileData).HasColumnName("idFileData");
                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .HasColumnName("file_name");
                entity.Property(e => e.FileType)
                    .HasMaxLength(32)
                    .HasColumnName("file_type");
                entity.Property(e => e.SizeByte).HasColumnName("size_byte");

                entity.HasOne(d => d.IdFileDataNavigation).WithMany(p => p.FileMetadata)
                    .HasForeignKey(d => d.IdFileData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("file_metadata_ibfk_1");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.IdGender).HasName("PRIMARY");

                entity.ToTable("gender");

                entity.Property(e => e.IdGender).HasColumnName("idGender");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<MedicalCard>(entity =>
            {
                entity.HasKey(e => e.IdMedicalCard).HasName("PRIMARY");

                entity.ToTable("medical_card");

                entity.HasIndex(e => e.IdBlood, "R_40");

                entity.HasIndex(e => e.IdPatient, "R_41");

                entity.Property(e => e.IdMedicalCard).HasColumnName("idMedicalCard");
                entity.Property(e => e.IdBlood).HasColumnName("idBlood");
                entity.Property(e => e.IdPatient).HasColumnName("idPatient");

                entity.HasOne(d => d.IdBloodNavigation).WithMany(p => p.MedicalCards)
                    .HasForeignKey(d => d.IdBlood)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("medical_card_ibfk_1");

                entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.MedicalCards)
                    .HasForeignKey(d => d.IdPatient)
                    .HasConstraintName("medical_card_ibfk_2");
            });

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(e => e.IdMedicalRecord).HasName("PRIMARY");

                entity.ToTable("medical_record");

                entity.HasIndex(e => e.IdMedicalCard, "R_42");

                entity.HasIndex(e => e.IdDoctor, "R_43");

                entity.Property(e => e.IdMedicalRecord).HasColumnName("idMedicalRecord");
                entity.Property(e => e.HasAttachments).HasColumnName("has_attachments");
                entity.Property(e => e.HasDiagnosis).HasColumnName("has_diagnosis");
                entity.Property(e => e.HasPrescriptions).HasColumnName("has_prescriptions");
                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
                entity.Property(e => e.IdMedicalCard).HasColumnName("idMedicalCard");
                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("medical_record_ibfk_2");

                entity.HasOne(d => d.IdMedicalCardNavigation).WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.IdMedicalCard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("medical_record_ibfk_1");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("PRIMARY");

                entity.ToTable("patient");

                entity.HasIndex(e => e.IdUser, "R_15");

                entity.HasIndex(e => e.IdGender, "R_19");

                entity.Property(e => e.IdPatient).HasColumnName("idPatient");
                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");
                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .HasColumnName("firstname");
                entity.Property(e => e.IdGender).HasColumnName("idGender");
                entity.Property(e => e.IdUser).HasColumnName("idUser");
                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .HasColumnName("lastname");

                entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Patients)
                    .HasForeignKey(d => d.IdGender)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("patient_ibfk_2");

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Patients)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("patient_ibfk_1");

                entity.HasMany(d => d.IdContactInfos).WithMany(p => p.IdPatients)
                    .UsingEntity<Dictionary<string, object>>(
                        "PatientContactInfo",
                        r => r.HasOne<ContactInfo>().WithMany()
                            .HasForeignKey("IdContactInfo")
                            .HasConstraintName("patient_contact_info_ibfk_2"),
                        l => l.HasOne<Patient>().WithMany()
                            .HasForeignKey("IdPatient")
                            .OnDelete(DeleteBehavior.Restrict)
                            .HasConstraintName("patient_contact_info_ibfk_1"),
                        j =>
                        {
                            j.HasKey("IdPatient", "IdContactInfo").HasName("PRIMARY");
                            j.ToTable("patient_contact_info");
                            j.HasIndex(new[] { "IdContactInfo" }, "R_24");
                            j.IndexerProperty<int>("IdPatient").HasColumnName("idPatient");
                            j.IndexerProperty<int>("IdContactInfo").HasColumnName("idContactInfo");
                        });
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => new { e.IdPrescription, e.IdMedicalRecord }).HasName("PRIMARY");

                entity.ToTable("prescription");

                entity.HasIndex(e => e.IdMedicalRecord, "R_46");

                entity.Property(e => e.IdPrescription)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idPrescription");
                entity.Property(e => e.IdMedicalRecord).HasColumnName("idMedicalRecord");
                entity.Property(e => e.AdverseReactions)
                    .HasMaxLength(255)
                    .HasColumnName("adverse_reactions");
                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("expiration_date");
                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("from_date");
                entity.Property(e => e.MedicineName)
                    .HasMaxLength(50)
                    .HasColumnName("medicine_name");
                entity.Property(e => e.MedicineStrength)
                    .HasMaxLength(20)
                    .HasColumnName("medicine_strength");
                entity.Property(e => e.RefillsCount).HasColumnName("refills_count");
                entity.Property(e => e.ReouteOfAdministration)
                    .HasMaxLength(20)
                    .HasColumnName("reoute_of_administration");
                entity.Property(e => e.StorageRequirements)
                    .HasMaxLength(255)
                    .HasColumnName("storage_requirements");

                entity.HasOne(d => d.IdMedicalRecordNavigation).WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.IdMedicalRecord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("prescription_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole).HasName("PRIMARY");

                entity.ToTable("role");

                entity.Property(e => e.IdRole).HasColumnName("idRole");
                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService).HasName("PRIMARY");

                entity.ToTable("service");

                entity.Property(e => e.IdService).HasColumnName("idService");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.IdShift).HasName("PRIMARY");

                entity.ToTable("shift");

                entity.HasIndex(e => e.IdDoctor, "R_37");

                entity.Property(e => e.IdShift).HasColumnName("idShift");
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");
                entity.Property(e => e.EndTime)
                    .HasColumnType("time")
                    .HasColumnName("end_time");
                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
                entity.Property(e => e.StartTime)
                    .HasColumnType("time")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.IdDoctor)
                    .HasConstraintName("shift_ibfk_1");
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.HasKey(e => e.IdSpeciality).HasName("PRIMARY");

                entity.ToTable("speciality");

                entity.Property(e => e.IdSpeciality).HasColumnName("idSpeciality");
                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.IdRole, "user_ibfk1_idx");

                entity.Property(e => e.IdUser).HasColumnName("idUser");
                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .HasColumnName("email");
                entity.Property(e => e.IdRole).HasColumnName("idRole");
                entity.Property(e => e.Login)
                    .HasMaxLength(128)
                    .HasColumnName("login");
                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .IsFixedLength()
                    .HasColumnName("password_hash");
                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .IsFixedLength()
                    .HasColumnName("password_salt");

                entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_ibfk1");
            });

            modelBuilder.Entity<WorkHistory>(entity =>
            {
                entity.HasKey(e => new { e.IdWorkHistory, e.IdDoctor }).HasName("PRIMARY");

                entity.ToTable("work_history");

                entity.HasIndex(e => e.IdDoctor, "R_42");

                entity.Property(e => e.IdWorkHistory)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idWorkHistory");
                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");
                entity.Property(e => e.Organization)
                    .HasMaxLength(50)
                    .HasColumnName("organization");
                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.WorkHistories)
                    .HasForeignKey(d => d.IdDoctor)
                    .HasConstraintName("work_history_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}