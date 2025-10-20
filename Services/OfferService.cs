using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repo;

        public OfferService(IOfferRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddOfferAsync(Offer offer)
        {
            if (offer == null) throw new System.ArgumentNullException(nameof(offer));
            await _repo.AddAsync(offer);
            await _repo.SaveAsync();
            return offer.Id;
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Offer offer)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = offer.Name;
            existing.Description = offer.Description;
            existing.Mode = offer.Mode;
            existing.Duration = offer.Duration;
            existing.SpecificRequirements = offer.SpecificRequirements;
            existing.MaximumQuota = offer.MaximumQuota;
            existing.StartDate = offer.StartDate;
            existing.EndDate = offer.EndDate;
            existing.UpdatedAt = System.DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
            return true;
        }
    }
}
