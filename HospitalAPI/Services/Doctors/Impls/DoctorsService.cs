using HospitalAPI.Database;
using HospitalAPI.Repositories.Doctors;
using HospitalAPI.Repositories.Specialities;
using HospitalAPI.Repositories.WorkHistories;

namespace HospitalAPI.Services.Doctors.Impls
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;
        private readonly ISpecialityRepository _specialityRepository;

        public DoctorsService(
            IDoctorRepository doctorRepository,
            IWorkHistoryRepository workHistoryRepository,
            ISpecialityRepository specialityRepository)
        {
            _doctorRepository = doctorRepository;
            _workHistoryRepository = workHistoryRepository;
            _specialityRepository = specialityRepository;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _doctorRepository.GetAsync();
        }

        public async Task<Doctor?> GetDoctorById(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<bool> HasDoctor(int id)
        {
            return await _doctorRepository.HasAsync(id);
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

        public async Task LoadDoctorSpecialities(Doctor doctor)
        {
            await _doctorRepository.LoadSpecialitiesAsync(doctor);
        }
    }
}