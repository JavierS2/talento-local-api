using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class PostulationRepository : IPostulationRepository
    {
        private readonly DbDevopsContext _context;

        public PostulationRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postulation>> GetAllAsync()
        {
            return await _context.Postulations
                .Include(p => p.Offer)                
                .Include(p => p.Status)               
                .Include(p => p.Evaluation)           
                .ToListAsync();
        }

        public async Task<Postulation?> GetByIdAsync(int id)
        {
            return await _context.Postulations
                .Include(p => p.Offer)
                .Include(p => p.Status)
                .Include(p => p.Evaluation)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Postulation postulation)
        {
            await _context.Postulations.AddAsync(postulation);
        }

        public Task UpdateAsync(Postulation postulation)
        {
            _context.Postulations.Update(postulation);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var postulation = await GetByIdAsync(id);
            if (postulation != null)
            {
                _context.Postulations.Remove(postulation);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
