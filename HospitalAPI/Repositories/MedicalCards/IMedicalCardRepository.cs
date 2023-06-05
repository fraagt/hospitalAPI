using HospitalAPI.Database;

namespace HospitalAPI.Repositories.MedicalCards
{
    public interface IMedicalCardRepository
    {
        Task<IEnumerable<MedicalCard>> GetAsync();
        Task<MedicalCard?> GetByIdAsync(int id);
        Task UpdateAsync(MedicalCard medicalCard);
    }
}