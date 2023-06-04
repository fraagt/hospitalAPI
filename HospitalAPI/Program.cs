using HospitalAPI.Database;
using HospitalAPI.Repositories.AppointmentTimes;
using HospitalAPI.Repositories.AppointmentTimes.Impls;
using HospitalAPI.Repositories.ContactInfos;
using HospitalAPI.Repositories.ContactInfos.Impls;
using HospitalAPI.Repositories.Doctors;
using HospitalAPI.Repositories.Doctors.Impls;
using HospitalAPI.Repositories.Services;
using HospitalAPI.Repositories.Services.Impls;
using HospitalAPI.Repositories.Shifts;
using HospitalAPI.Repositories.Shifts.Impls;
using HospitalAPI.Repositories.Specialities;
using HospitalAPI.Repositories.Specialities.Impls;
using HospitalAPI.Repositories.WorkHistories;
using HospitalAPI.Repositories.WorkHistories.Impls;
using HospitalAPI.Services.Doctors;
using HospitalAPI.Services.Doctors.Impls;

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

//Services
builder.Services.AddScoped<IDoctorsService, DoctorsService>();

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