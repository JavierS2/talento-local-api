using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories
{
    public class OfferCategoryRepository : IOfferCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public OfferCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OfferCategory>> GetAllAsync()
        {
            return await _context.OfferCategories
                .Include(c => c.Offers)
                .ToListAsync();
        }

        public async Task<OfferCategory?> GetByIdAsync(int id)
        {
            return await _context.OfferCategories
                .Include(c => c.Offers)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(OfferCategory offerCategory)
        {
            await _context.OfferCategories.AddAsync(offerCategory);
        }

        public Task UpdateAsync(OfferCategory offerCategory)
        {
            _context.OfferCategories.Update(offerCategory);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var offerCategory = await GetByIdAsync(id);
            if (offerCategory != null)
            {
                _context.OfferCategories.Remove(offerCategory);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
