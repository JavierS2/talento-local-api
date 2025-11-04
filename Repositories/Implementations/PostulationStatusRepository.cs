using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class PostulationStatusRepository : IPostulationStatusRepository
    {
        private readonly DbDevopsContext _context;

        public PostulationStatusRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<List<PostulationStatus>> GetAllAsync()
        {
            return await _context.PostulationStatus
                .Include(s => s.Postulations)
                .ToListAsync();
        }

        public async Task<PostulationStatus?> GetByIdAsync(int id)
        {
            return await _context.PostulationStatus
                .Include(s => s.Postulations)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(PostulationStatus postulationStatus)
        {
            await _context.PostulationStatus.AddAsync(postulationStatus);
        }

        public Task UpdateAsync(PostulationStatus postulationStatus)
        {
            _context.PostulationStatus.Update(postulationStatus);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var status = await GetByIdAsync(id);
            if (status != null)
            {
                _context.PostulationStatus.Remove(status);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
