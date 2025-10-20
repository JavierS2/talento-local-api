using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services
{
    public class OfferService : IOfferService
    {
        private readonly DbDevopsContext _db;

        public OfferService(DbDevopsContext db)
        {
            _db = db;
        }

        public async Task<int> AddOfferAsync(Offer offer)
        {
            if (offer == null) throw new System.ArgumentNullException(nameof(offer));
            _db.Offers.Add(offer);
            await _db.SaveChangesAsync();
            return offer.Id;
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await _db.Offers
                .Include(o => o.Convocation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _db.Offers
                .Include(o => o.Convocation)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, Offer offer)
        {
            var existing = await _db.Offers.FindAsync(id);
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

            _db.Offers.Update(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
