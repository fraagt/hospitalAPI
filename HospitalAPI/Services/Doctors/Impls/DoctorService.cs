using HospitalAPI.Database;
using HospitalAPI.Repositories.Doctors;
using HospitalAPI.Repositories.WorkHistories;

namespace HospitalAPI.Services.Doctors.Impls
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public DoctorService(
            IDoctorRepository doctorRepository,
            IWorkHistoryRepository workHistoryRepository
        )
        {
            _doctorRepository = doctorRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _doctorRepository.GetDoctorsAsync();
        }

        public async Task<Doctor?> GetDoctorById(int id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }

        public async Task<bool> HasDoctor(int id)
        {
            return await _doctorRepository.HasDoctorAsync(id);
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            await _doctorRepository.UpdateDoctorAsync(doctor);
        }

        public async Task<IEnumerable<WorkHistory>> GetWorkHistories()
        {
            return await _workHistoryRepository.GetWorkHistoriesAsync();
        }

        public async Task CreateWorkHistory(WorkHistory workHistory)
        {
            await _workHistoryRepository.CreateWorkHistoryAsync(workHistory);
        }
    }
}