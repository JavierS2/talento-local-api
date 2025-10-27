using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class ConvocationRepository : IConvocationRepository
    {
        private readonly DbDevopsContext _context;

        public ConvocationRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<List<Convocation>?> GetAllAsync()
        {
            return await _context.Convocations
                .Include(c => c.PublishingEntity)
                .Include(c => c.Offers)
                .Include(c => c.Postulations)
                .ToListAsync();
        }

        public async Task<Convocation?> GetByIdAsync(int id)
        {
            return await _context.Convocations
                .Include(c => c.PublishingEntity)
                .Include(c => c.Offers)
                .Include(c => c.Postulations)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Convocation?> GetByStatusAsync(ConvocationStatus status)
        {
            return await _context.Convocations
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.State == status);
        }

        public async Task<IEnumerable<Convocation>> SearchByLocationAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) return new List<Convocation>();
            var pattern = location.Trim().ToLower();
            return await _context.Convocations
                .Where(c => c.Location.ToLower().Contains(pattern))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Convocation>> SearchByAvailablePlacesAsync(int minAvailablePlaces)
        {
            return await _context.Convocations
                .Where(c => c.AvailablePlaces >= minAvailablePlaces)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Convocation convocation)
        {
            await _context.Convocations.AddAsync(convocation);
        }

        public Task UpdateAsync(Convocation convocation)
        {
            _context.Convocations.Update(convocation);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var convocation = await GetByIdAsync(id);
            if (convocation != null)
            {
                _context.Convocations.Remove(convocation);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
