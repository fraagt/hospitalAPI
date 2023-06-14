using HospitalAPI.Database;

namespace HospitalAPI.Services.Patients
{
    public interface IPatientsService
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient?> GetPatientById(int id);
        Task CreatePatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task<IEnumerable<ContactInfo>> GetContactInfosByPatient(Patient patient);
        Task<ContactInfo?> GetContactInfoById(int id);
        Task CreateContactInfo(ContactInfo contactInfo);
        Task DeleteContactInfo(ContactInfo contactInfo);
    }
}