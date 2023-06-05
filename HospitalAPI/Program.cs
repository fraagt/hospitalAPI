using HospitalAPI.Database;
using HospitalAPI.Repositories.Allergens;
using HospitalAPI.Repositories.Allergens.Impls;
using HospitalAPI.Repositories.Allergies;
using HospitalAPI.Repositories.Allergies.Impls;
using HospitalAPI.Repositories.Appointments;
using HospitalAPI.Repositories.Appointments.Impls;
using HospitalAPI.Repositories.AppointmentStatusChanges;
using HospitalAPI.Repositories.AppointmentStatusChanges.Impls;
using HospitalAPI.Repositories.AppointmentStatuses;
using HospitalAPI.Repositories.AppointmentStatuses.Impls;
using HospitalAPI.Repositories.AppointmentTimes;
using HospitalAPI.Repositories.AppointmentTimes.Impls;
using HospitalAPI.Repositories.Attachments;
using HospitalAPI.Repositories.Attachments.Impls;
using HospitalAPI.Repositories.Bloods;
using HospitalAPI.Repositories.Bloods.Impls;
using HospitalAPI.Repositories.ContactInfos;
using HospitalAPI.Repositories.ContactInfos.Impls;
using HospitalAPI.Repositories.Diagnoses;
using HospitalAPI.Repositories.Diagnoses.Impls;
using HospitalAPI.Repositories.Doctors;
using HospitalAPI.Repositories.Doctors.Impls;
using HospitalAPI.Repositories.MedicalCards;
using HospitalAPI.Repositories.MedicalCards.Impls;
using HospitalAPI.Repositories.MedicalRecords;
using HospitalAPI.Repositories.MedicalRecords.Impls;
using HospitalAPI.Repositories.Patients;
using HospitalAPI.Repositories.Patients.Impls;
using HospitalAPI.Repositories.Prescriptions;
using HospitalAPI.Repositories.Prescriptions.Impls;
using HospitalAPI.Repositories.Services;
using HospitalAPI.Repositories.Services.Impls;
using HospitalAPI.Repositories.Shifts;
using HospitalAPI.Repositories.Shifts.Impls;
using HospitalAPI.Repositories.Specialities;
using HospitalAPI.Repositories.Specialities.Impls;
using HospitalAPI.Repositories.WorkHistories;
using HospitalAPI.Repositories.WorkHistories.Impls;
using HospitalAPI.Services.Appointments;
using HospitalAPI.Services.Appointments.Impls;
using HospitalAPI.Services.Doctors;
using HospitalAPI.Services.Doctors.Impls;
using HospitalAPI.Services.MedicalCards;
using HospitalAPI.Services.MedicalCards.Impls;
using HospitalAPI.Services.Patients;
using HospitalAPI.Services.Patients.Impls;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
builder.Services.AddDbContext<HospitalContext>();

//Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Repositories
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IWorkHistoryRepository, WorkHistoryRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<IAppointmentTimeRepository, AppointmentTimeRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentStatusRepository, AppointmentStatusRepository>();
builder.Services.AddScoped<IAppointmentStatusChangeRepository, AppointmentStatusChangeRepository>();
builder.Services.AddScoped<IMedicalCardRepository, MedicalCardRepository>();
builder.Services.AddScoped<IBloodRepository, BloodRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAllergenRepository, AllergenRepository>();
builder.Services.AddScoped<IAllergyRepository, AllergyRepository>();

//Services
builder.Services.AddScoped<IDoctorsService, DoctorsService>();
builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();
builder.Services.AddScoped<IMedicalCardsService, MedicalCardsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();