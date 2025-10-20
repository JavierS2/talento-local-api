using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IPublishingEntityService
    {
        Task<int> AddPublishingEntityAsync(PublishingEntity publishingEntity);
        Task<IEnumerable<PublishingEntity>> GetAllAsync();
        Task<PublishingEntity?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, PublishingEntity publishingEntity);
        Task<bool> DeleteAsync(int id);
    }
}
