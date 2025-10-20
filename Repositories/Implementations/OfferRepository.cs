using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _context.Offers.FirstOrDefaultAsync(o => o.Id == id);
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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
