using System.Collections.Generic;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
	public interface IPublishingEntityRepository
	{
		IEnumerable<PublishingEntity> GetAll();
		PublishingEntity? GetById(int id);
		void Add(PublishingEntity publishingEntity);
		void Update(PublishingEntity publishingEntity);
		void Delete(int id);
		void Save();
	}
}
