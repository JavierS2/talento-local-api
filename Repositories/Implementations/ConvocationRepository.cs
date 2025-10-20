using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class ConvocationRepository : IConvocationRepository
    {
        private readonly DbDevopsContext _context;

        public ConvocationRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public IEnumerable<Convocation> GetAll()
        {
            return _context.Convocations
                .Include(c => c.PublishingEntity)
                .Include(c => c.Offers)
                .Include(c => c.Postulations)
                .Include(c => c.Histories)
                .ToList();
        }

        public Convocation? GetById(int id)
        {
            return _context.Convocations
                .Include(c => c.PublishingEntity)
                .Include(c => c.Offers)
                .Include(c => c.Postulations)
                .Include(c => c.Histories)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Convocation convocation)
        {
            _context.Convocations.Add(convocation);
        }

        public void Update(Convocation convocation)
        {
            _context.Convocations.Update(convocation);
        }

        public void Delete(int id)
        {
            var convocation = GetById(id);
            if (convocation != null)
            {
                _context.Convocations.Remove(convocation);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
