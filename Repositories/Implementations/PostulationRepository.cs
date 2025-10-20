using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class PostulationRepository : IPostulationRepository
    {
        private readonly DbDevopsContext _context;

        public PostulationRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public IEnumerable<Postulation> GetAll()
        {
            return _context.Postulations
                .Include(p => p.Convocation)
                .Include(p => p.Evaluation)
                .ToList();
        }

        public Postulation? GetById(int id)
        {
            return _context.Postulations
                .Include(p => p.Convocation)
                .Include(p => p.Evaluation)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Add(Postulation postulation)
        {
            _context.Postulations.Add(postulation);
        }

        public void Update(Postulation postulation)
        {
            _context.Postulations.Update(postulation);
        }

        public void Delete(int id)
        {
            var postulation = GetById(id);
            if (postulation != null)
            {
                _context.Postulations.Remove(postulation);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
