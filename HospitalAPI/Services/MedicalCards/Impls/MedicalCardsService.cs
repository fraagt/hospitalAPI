using HospitalAPI.Database;
using HospitalAPI.Repositories.Allergens;
using HospitalAPI.Repositories.Allergies;
using HospitalAPI.Repositories.Attachments;
using HospitalAPI.Repositories.Bloods;
using HospitalAPI.Repositories.Diagnoses;
using HospitalAPI.Repositories.MedicalCards;
using HospitalAPI.Repositories.MedicalRecords;
using HospitalAPI.Repositories.Prescriptions;

namespace HospitalAPI.Services.MedicalCards.Impls
{
    public class MedicalCardsService : IMedicalCardsService
    {
        private readonly IMedicalCardRepository _medicalCardRepository;
        private readonly IBloodRepository _bloodRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IDiagnosisRepository _diagnosisRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IAllergenRepository _allergenRepository;
        private readonly IAllergyRepository _allergyRepository;

        public MedicalCardsService(
            IMedicalCardRepository medicalCardRepository,
            IBloodRepository bloodRepository,
            IMedicalRecordRepository medicalRecordRepository,
            IPrescriptionRepository prescriptionRepository,
            IDiagnosisRepository diagnosisRepository,
            IAttachmentRepository attachmentRepository,
            IAllergenRepository allergenRepository,
            IAllergyRepository allergyRepository
        )
        {
            _medicalCardRepository = medicalCardRepository;
            _bloodRepository = bloodRepository;
            _medicalRecordRepository = medicalRecordRepository;
            _prescriptionRepository = prescriptionRepository;
            _diagnosisRepository = diagnosisRepository;
            _attachmentRepository = attachmentRepository;
            _allergenRepository = allergenRepository;
            _allergyRepository = allergyRepository;
        }
        
        public async Task<IEnumerable<MedicalCard>> GetMedicalCards()
        {
            return await _medicalCardRepository.GetAsync();
        }

        public async Task<MedicalCard?> GetMedicalCardById(int id)
        {
            return await _medicalCardRepository.GetByIdAsync(id);
        }

        public async Task UpdateMedicalCard(MedicalCard medicalCard)
        {
            await _medicalCardRepository.UpdateAsync(medicalCard);
        }

        public async Task<IEnumerable<Blood>> GetBloods()
        {
            return await _bloodRepository.GetAsync();
        }

        public async Task<Blood?> GetBloodById(int id)
        {
            return await _bloodRepository.GetByIdAsync(id);
        }

        public async Task CreateBlood(Blood blood)
        {
            await _bloodRepository.CreateAsync(blood);
        }

        public async Task DeleteBlood(Blood blood)
        {
            await _bloodRepository.DeleteAsync(blood);
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecords()
        {
            return await _medicalRecordRepository.GetAsync();
        }

        public async Task<MedicalRecord?> GetMedicalRecordById(int id)
        {
            return await _medicalRecordRepository.GetByIdAsync(id);
        }

        public async Task CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            await _medicalRecordRepository.CreateAsync(medicalRecord);
        }

        public async Task UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            await _medicalRecordRepository.UpdateAsync(medicalRecord);
        }

        public async Task DeleteMedicalRecord(MedicalRecord medicalRecord)
        {
            await _medicalRecordRepository.DeleteAsync(medicalRecord);
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptions()
        {
            return await _prescriptionRepository.GetAsync();
        }

        public async Task<Prescription?> GetPrescriptionById(int id)
        {
            return await _prescriptionRepository.GetByIdAsync(id);
        }

        public async Task CreatePrescription(Prescription prescription)
        {
            await _prescriptionRepository.CreateAsync(prescription);
        }

        public async Task UpdatePrescription(Prescription prescription)
        {
            await _prescriptionRepository.UpdateAsync(prescription);
        }

        public async Task DeletePrescription(Prescription prescription)
        {
            await _prescriptionRepository.DeleteAsync(prescription);
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnoses()
        {
            return await _diagnosisRepository.GetAsync();
        }

        public async Task<Diagnosis?> GetDiagnosisById(int id)
        {
            return await _diagnosisRepository.GetByIdAsync(id);
        }

        public async Task CreateDiagnosis(Diagnosis diagnosis)
        {
            await _diagnosisRepository.CreateAsync(diagnosis);
        }

        public async Task UpdateDiagnosis(Diagnosis diagnosis)
        {
            await _diagnosisRepository.UpdateAsync(diagnosis);
        }

        public async Task DeleteDiagnosis(Diagnosis diagnosis)
        {
            await _diagnosisRepository.DeleteAsync(diagnosis);
        }

        public async Task<IEnumerable<Attachment>> GetAttachments()
        {
            return await _attachmentRepository.GetAsync();
        }

        public async Task<Attachment?> GetAttachmentById(int id)
        {
            return await _attachmentRepository.GetByIdAsync(id);
        }

        public async Task CreateAttachment(Attachment attachment)
        {
            await _attachmentRepository.CreateAsync(attachment);
        }

        public async Task UpdateAttachment(Attachment attachment)
        {
            await _attachmentRepository.UpdateAsync(attachment);
        }

        public async Task DeleteAttachment(Attachment attachment)
        {
            await _attachmentRepository.DeleteAsync(attachment);
        }

        public async Task<IEnumerable<Allergy>> GetAllergies()
        {
            return await _allergyRepository.GetAsync();
        }

        public async Task<Allergy?> GetAllergyById(int id)
        {
            return await _allergyRepository.GetByIdAsync(id);
        }

        public async Task CreateAllergy(Allergy allergy)
        {
            await _allergyRepository.CreateAsync(allergy);
        }

        public async Task UpdateAllergy(Allergy allergy)
        {
            await _allergyRepository.UpdateAsync(allergy);
        }

        public async Task DeleteAllergy(Allergy allergy)
        {
            await _allergyRepository.DeleteAsync(allergy);
        }

        public async Task<IEnumerable<Allergen>> GetAllergens()
        {
            return await _allergenRepository.GetAsync();
        }

        public async Task<Allergen?> GetAllergenById(int id)
        {
            return await _allergenRepository.GetByIdAsync(id);
        }

        public async Task CreateAllergen(Allergen allergen)
        {
            await _allergenRepository.CreateAsync(allergen);
        }

        public async Task DeleteAllergen(Allergen allergen)
        {
            await _allergenRepository.DeleteAsync(allergen);
        }
    }
}