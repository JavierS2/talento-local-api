using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class PublishingEntityRepository : IPublishingEntityRepository
    {
        private readonly DbDevopsContext _context;

        public PublishingEntityRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PublishingEntity>> GetAllAsync()
        {
            return await _context.PublishingEntities
                .Include(pe => pe.Convocations)
                .ToListAsync();
        }

        public async Task<PublishingEntity?> GetByIdAsync(int id)
        {
            return await _context.PublishingEntities
                .Include(pe => pe.Convocations)
                .FirstOrDefaultAsync(pe => pe.Id == id);
        }

        public async Task AddAsync(PublishingEntity publishingEntity)
        {
            await _context.PublishingEntities.AddAsync(publishingEntity);
        }

        public Task UpdateAsync(PublishingEntity publishingEntity)
        {
            _context.PublishingEntities.Update(publishingEntity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.PublishingEntities.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
