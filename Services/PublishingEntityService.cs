using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services
{
    public class PublishingEntityService : IPublishingEntityService
    {
        private readonly DbDevopsContext _db;

        public PublishingEntityService(DbDevopsContext db)
        {
            _db = db;
        }

        public async Task<int> AddPublishingEntityAsync(PublishingEntity publishingEntity)
        {
            _db.PublishingEntities.Add(publishingEntity);
            await _db.SaveChangesAsync();
            return publishingEntity.Id;
        }

        public async Task<IEnumerable<PublishingEntity>> GetAllAsync()
        {
            return await _db.PublishingEntities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PublishingEntity?> GetByIdAsync(int id)
        {
            return await _db.PublishingEntities
                .AsNoTracking()
                .FirstOrDefaultAsync(pe => pe.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, PublishingEntity publishingEntity)
        {
            var existing = await _db.PublishingEntities.FindAsync(id);
            if (existing == null) return false;

            existing.Name = publishingEntity.Name;
            existing.Type = publishingEntity.Type;
            existing.Description = publishingEntity.Description;
            existing.Email = publishingEntity.Email;
            existing.Phone = publishingEntity.Phone;
            existing.SiteWeb = publishingEntity.SiteWeb;
            existing.Address = publishingEntity.Address;
            existing.UpdateAt = DateTime.UtcNow;

            _db.PublishingEntities.Update(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.PublishingEntities.FindAsync(id);
            if (existing == null) return false;
            _db.PublishingEntities.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
