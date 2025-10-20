using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
	public interface IPublishingEntityRepository
	{
		Task<IEnumerable<PublishingEntity>> GetAllAsync();
		Task<PublishingEntity?> GetByIdAsync(int id);
		Task AddAsync(PublishingEntity publishingEntity);
		Task UpdateAsync(PublishingEntity publishingEntity);
		Task DeleteAsync(int id);
		Task SaveAsync();
	}
}
