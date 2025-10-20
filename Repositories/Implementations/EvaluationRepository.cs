using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly DbDevopsContext _context;

        public EvaluationRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public IEnumerable<Evaluation> GetAll()
        {
            return _context.Evaluations
                .Include(e => e.Postulation)
                .ToList();
        }

        public Evaluation? GetById(int id)
        {
            return _context.Evaluations
                .Include(e => e.Postulation)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(Evaluation evaluation)
        {
            _context.Evaluations.Add(evaluation);
        }

        public void Update(Evaluation evaluation)
        {
            _context.Evaluations.Update(evaluation);
        }

        public void Delete(int id)
        {
            var evaluation = GetById(id);
            if (evaluation != null)
            {
                _context.Evaluations.Remove(evaluation);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
