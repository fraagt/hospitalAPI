using HospitalAPI.Database;
using HospitalAPI.Models.ContactInfos;
using HospitalAPI.Repositories.ContactInfos;
using HospitalAPI.Repositories.Patients;

namespace HospitalAPI.Services.Patients.Impls
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContactInfoRepository _contactInfoRepository;

        public PatientsService(
            IPatientRepository patientRepository,
            IContactInfoRepository contactInfoRepository
        )
        {
            _patientRepository = patientRepository;
            _contactInfoRepository = contactInfoRepository;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _patientRepository.GetAsync();
        }

        public async Task<Patient?> GetPatientById(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task CreatePatient(Patient patient)
        {
            await _patientRepository.CreateAsync(patient);
        }

        public async Task UpdatePatient(Patient patient)
        {
            await _patientRepository.Update(patient);
        }

        public async Task<IEnumerable<ContactInfo>> GetContactInfosByPatient(Patient patient)
        {
            var filter = new ContactInfoFilter {PatientId = patient.IdPatient};
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
    }
}