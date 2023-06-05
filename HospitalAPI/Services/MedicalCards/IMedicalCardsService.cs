using HospitalAPI.Database;

namespace HospitalAPI.Services.MedicalCards
{
    public interface IMedicalCardsService
    {
        Task<IEnumerable<MedicalCard>> GetMedicalCards();
        Task<MedicalCard?> GetMedicalCardById(int id);
        Task UpdateMedicalCard(MedicalCard medicalCard);
        Task<IEnumerable<Blood>> GetBloods();
        Task<Blood?> GetBloodById(int id);
        Task CreateBlood(Blood blood);
        Task DeleteBlood(Blood blood);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecords();
        Task<MedicalRecord?> GetMedicalRecordById(int id);
        Task CreateMedicalRecord(MedicalRecord medicalRecord);
        Task UpdateMedicalRecord(MedicalRecord medicalRecord);
        Task DeleteMedicalRecord(MedicalRecord medicalRecord);
        Task<IEnumerable<Prescription>> GetPrescriptions();
        Task<Prescription?> GetPrescriptionById(int id);
        Task CreatePrescription(Prescription prescription);
        Task UpdatePrescription(Prescription prescription);
        Task DeletePrescription(Prescription prescription);
        Task<IEnumerable<Diagnosis>> GetDiagnoses();
        Task<Diagnosis?> GetDiagnosisById(int id);
        Task CreateDiagnosis(Diagnosis diagnosis);
        Task UpdateDiagnosis(Diagnosis diagnosis);
        Task DeleteDiagnosis(Diagnosis diagnosis);
        Task<IEnumerable<Attachment>> GetAttachments();
        Task<Attachment?> GetAttachmentById(int id);
        Task CreateAttachment(Attachment attachment);
        Task UpdateAttachment(Attachment attachment);
        Task DeleteAttachment(Attachment attachment);
        Task<IEnumerable<Allergy>> GetAllergies();
        Task<Allergy?> GetAllergyById(int id);
        Task CreateAllergy(Allergy allergy);
        Task UpdateAllergy(Allergy allergy);
        Task DeleteAllergy(Allergy allergy);
        Task<IEnumerable<Allergen>> GetAllergens();
        Task<Allergen?> GetAllergenById(int id);
        Task CreateAllergen(Allergen allergen);
        Task DeleteAllergen(Allergen allergen);
    }
}