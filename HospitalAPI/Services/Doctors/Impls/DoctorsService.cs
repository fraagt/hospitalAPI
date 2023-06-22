using HospitalAPI.Database;
using HospitalAPI.Models.AppointmentTimes;
using HospitalAPI.Models.ContactInfos;
using HospitalAPI.Models.Doctors;
using HospitalAPI.Repositories.AppointmentTimes;
using HospitalAPI.Repositories.ContactInfos;
using HospitalAPI.Repositories.Doctors;
using HospitalAPI.Repositories.Services;
using HospitalAPI.Repositories.Shifts;
using HospitalAPI.Repositories.Specialities;
using HospitalAPI.Repositories.WorkHistories;

namespace HospitalAPI.Services.Doctors.Impls
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IAppointmentTimeRepository _appointmentTimeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IShiftRepository _shiftRepository;
        private readonly IContactInfoRepository _contactInfoRepository;

        public DoctorsService(
            IDoctorRepository doctorRepository,
            IWorkHistoryRepository workHistoryRepository,
            ISpecialityRepository specialityRepository,
            IAppointmentTimeRepository appointmentTimeRepository,
            IServiceRepository serviceRepository,
            IShiftRepository shiftRepository,
            IContactInfoRepository contactInfoRepository
            )
        {
            _doctorRepository = doctorRepository;
            _workHistoryRepository = workHistoryRepository;
            _specialityRepository = specialityRepository;
            _appointmentTimeRepository = appointmentTimeRepository;
            _serviceRepository = serviceRepository;
            _shiftRepository = shiftRepository;
            _contactInfoRepository = contactInfoRepository;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors(GetDoctorsFilters filters)
        {
            return await _doctorRepository.GetAsync(filters);
        }

        public async Task<Doctor?> GetDoctorById(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<bool> HasDoctor(int id)
        {
            return await _doctorRepository.HasAsync(id);
        }

        public async Task CreateDoctor(Doctor doctor)
        {
            await _doctorRepository.CreateAsync(doctor);
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task<IEnumerable<WorkHistory>> GetWorkHistories()
        {
            return await _workHistoryRepository.GetAsync();
        }

        public async Task CreateWorkHistory(WorkHistory workHistory)
        {
            await _workHistoryRepository.CreateAsync(workHistory);
        }

        public async Task<bool> HasWorkHistory(int id)
        {
            return await _workHistoryRepository.HasAsync(id);
        }

        public async Task<WorkHistory?> GetWorkHistoryById(int id)
        {
            return await _workHistoryRepository.GetByIdAsync(id);
        }

        public async Task DeleteWorkHistory(WorkHistory workHistory)
        {
            await _workHistoryRepository.DeleteAsync(workHistory);
        }

        public async Task<IEnumerable<Speciality>> GetSpecialities()
        {
            return await _specialityRepository.GetAsync();
        }

        public async Task CreateSpeciality(Speciality speciality)
        {
            await _specialityRepository.CreateAsync(speciality);
        }

        public async Task<Speciality?> GetSpecialityById(int id)
        {
            return await _specialityRepository.GetByIdAsync(id);
        }

        public async Task DeleteSpeciality(Speciality speciality)
        {
            await _specialityRepository.DeleteAsync(speciality);
        }

        public async Task<IEnumerable<AppointmentTime>> GetAppointmentTimes(
            AppointmentTimeFilters filters)
        {
            return await _appointmentTimeRepository.GetAsync(filters);
        }

        public async Task CreateAppointmentTime(AppointmentTime appointmentTime)
        {
            await _appointmentTimeRepository.CreateAsync(appointmentTime);
        }

        public async Task<AppointmentTime?> GetAppointmentTimeById(int id)
        {
            return await _appointmentTimeRepository.GetByIdAsync(id);
        }

        public async Task DeleteAppointmentTime(AppointmentTime appointmentTime)
        {
            await _appointmentTimeRepository.DeleteAsync(appointmentTime);
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _serviceRepository.GetAsync();
        }

        public async Task<Service?> GetServiceById(int id)
        {
            return await _serviceRepository.GetByIdAsync(id);
        }

        public async Task CreateService(Service service)
        {
            await _serviceRepository.CreateAsync(service);
        }

        public async Task DeleteService(Service service)
        {
            await _serviceRepository.DeleteAsync(service);
        }

        public async Task<IEnumerable<Shift>> GetShifts()
        {
            return await _shiftRepository.GetAsync();
        }

        public async Task<Shift?> GetShiftById(int id)
        {
            return await _shiftRepository.GetByIdAsync(id);
        }

        public async Task CreateShift(Shift shift)
        {
            await _shiftRepository.CreateAsync(shift);
        }

        public async Task DeleteShift(Shift shift)
        {
            await _shiftRepository.DeleteAsync(shift);
        }

        public async Task<IEnumerable<ContactInfo>> GetContactInfosByDoctor(Doctor doctor)
        {
            var filter = new ContactInfoFilter {DoctorId = doctor.IdDoctor};
            return await _contactInfoRepository.GetAsync(filter);
        }

        public async Task<ContactInfo?> GetContactInfoById(int id)
        {
            return await _contactInfoRepository.GetByIdAsync(id);
        }

        public async Task CreateContactInfo(ContactInfo contactInfo)
        {
            await _contactInfoRepository.CreateAsync(contactInfo);
        }

        public async Task DeleteContactInfo(ContactInfo contactInfo)
        {
            await _contactInfoRepository.DeleteAsync(contactInfo);
        }

        public async Task LoadDoctorSpecialities(Doctor doctor)
        {
            await _doctorRepository.LoadSpecialitiesAsync(doctor);
        }

        public async Task LoadDoctorServices(Doctor doctor)
        {
            await _doctorRepository.LoadServicesAsync(doctor);
        }
    }
}