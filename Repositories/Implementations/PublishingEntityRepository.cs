using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<PublishingEntity> GetAll()
        {
            return _context.PublishingEntities
                .Include(pe => pe.Convocations)
                .ToList();
        }

        public PublishingEntity? GetById(int id)
        {
            return _context.PublishingEntities
                .Include(pe => pe.Convocations)
                .FirstOrDefault(pe => pe.Id == id);
        }

        public void Add(PublishingEntity publishingEntity)
        {
            _context.PublishingEntities.Add(publishingEntity);
        }

        public void Update(PublishingEntity publishingEntity)
        {
            _context.PublishingEntities.Update(publishingEntity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.PublishingEntities.Remove(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
