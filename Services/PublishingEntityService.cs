using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Services
{
    public class PublishingEntityService : IPublishingEntityService
    {
        private readonly IPublishingEntityRepository _repo;

        public PublishingEntityService(IPublishingEntityRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddPublishingEntityAsync(PublishingEntity publishingEntity)
        {
            await _repo.AddAsync(publishingEntity);
            await _repo.SaveAsync();
            return publishingEntity.Id;
        }

        public async Task<List<PublishingEntity>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PublishingEntity?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, PublishingEntity publishingEntity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = publishingEntity.Name;
            existing.Type = publishingEntity.Type;
            existing.Description = publishingEntity.Description;
            existing.Email = publishingEntity.Email;
            existing.Phone = publishingEntity.Phone;
            existing.SiteWeb = publishingEntity.SiteWeb;
            existing.Address = publishingEntity.Address;
            existing.UpdateAt = System.DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
            return true;
        }
    }
}
