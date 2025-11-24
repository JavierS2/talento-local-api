using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class OfferRepository : IOfferRepository
    {
        private readonly DbDevopsContext _context;

        public OfferRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _context.Offers
                .Include(o => o.Category)
                .Include(o => o.Postulations)
                .ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _context.Offers
                .Include(o => o.Category)
                .Include(o => o.Postulations)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Offer offer)
        {
            await _context.Offers.AddAsync(offer);
        }

        public Task UpdateAsync(Offer offer)
        {
            _context.Offers.Update(offer);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var offer = await GetByIdAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Offer>> GetOffersByUserIdAsync(string userId)
        {
            return await _context.Offers
                .Include(o => o.Category)
                .Include(o => o.Postulations)
                .Where(o => !o.Postulations!.Any(p => p.UserId == userId))
                .ToListAsync();
        }

        public async Task<List<Offer>> GetByCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<Offer>();

            var normalized = category.Trim().ToLowerInvariant();

            return await _context.Offers
                .Include(o => o.Category)
                .Include(o => o.Postulations)
                .Where(o => o.Category != null && o.Category.Name.ToLower() == normalized)
                .ToListAsync();
        }

        public async Task<List<Offer>> GetByCompanyIdAsync(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
                return new List<Offer>();

            var normalized = companyId.Trim().ToLowerInvariant();

            return await _context.Offers
                .Include(o => o.Category)
                .Include(o => o.Postulations)
                .Where(o => o.CompanyId != null && o.CompanyId.ToLower() == normalized)
                .ToListAsync();
        }

        public async Task<int> CountByOffer(int offerId)
        {
            return await _context.Postulations
                .Where(p => p.OfferId == offerId)
                .CountAsync();
        }

        public async Task<int> CountByOfferAndStatus(int offerId, string statusName)
        {
            return await _context.Postulations
                .Where(p => p.OfferId == offerId && p.Status.Name == statusName)
                .CountAsync();
        }

    }
}
