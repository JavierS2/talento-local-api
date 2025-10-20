using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly DbDevopsContext _context;

        public HistoryRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<History>> GetAllAsync()
        {
            return await _context.Histories.ToListAsync();
        }

        public async Task<History?> GetByIdAsync(int id)
        {
            return await _context.Histories.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(History history)
        {
            await _context.Histories.AddAsync(history);
        }

        public Task UpdateAsync(History history)
        {
            _context.Histories.Update(history);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var history = await GetByIdAsync(id);
            if (history != null)
            {
                _context.Histories.Remove(history);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
