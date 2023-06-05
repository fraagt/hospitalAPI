using HospitalAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositories.MedicalCards.Impls
{
    public class MedicalCardRepository : IMedicalCardRepository
    {
        private readonly HospitalContext _hospitalContext;
        private readonly DbSet<MedicalCard> _medicalCards;

        public MedicalCardRepository(
            HospitalContext hospitalContext
        )
        {
            _hospitalContext = hospitalContext;
            _medicalCards = hospitalContext.MedicalCards;
        }
        
        public async Task<IEnumerable<MedicalCard>> GetAsync()
        {
            return await _medicalCards.ToListAsync();
        }

        public async Task<MedicalCard?> GetByIdAsync(int id)
        {
            return await _medicalCards.FirstOrDefaultAsync(card => card.IdMedicalCard == id);
        }

        public async Task UpdateAsync(MedicalCard medicalCard)
        {
            await _medicalCards.AddAsync(medicalCard);
            await _hospitalContext.SaveChangesAsync();
        }
    }
}