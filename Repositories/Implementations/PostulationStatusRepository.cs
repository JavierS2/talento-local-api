using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories
{
    public class PostulationStatusRepository : IPostulationStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public PostulationStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostulationStatus>> GetAllAsync()
        {
            return await _context.PostulationsStatus
                .Include(s => s.Postulations)
                .ToListAsync();
        }

        public async Task<PostulationStatus?> GetByIdAsync(int id)
        {
            return await _context.PostulationsStatus
                .Include(s => s.Postulations)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(PostulationStatus postulationStatus)
        {
            await _context.PostulationsStatus.AddAsync(postulationStatus);
        }

        public Task UpdateAsync(PostulationStatus postulationStatus)
        {
            _context.PostulationsStatus.Update(postulationStatus);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var status = await GetByIdAsync(id);
            if (status != null)
            {
                _context.PostulationsStatus.Remove(status);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
