using System.Collections.Generic;
using System.Linq;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly DbDevopsContext _context;

        public HistoryRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public IEnumerable<History> GetAll()
        {
            return _context.Histories.ToList();
        }

        public History? GetById(int id)
        {
            return _context.Histories.FirstOrDefault(h => h.Id == id);
        }

        public void Add(History history)
        {
            _context.Histories.Add(history);
        }

        public void Update(History history)
        {
            _context.Histories.Update(history);
        }

        public void Delete(int id)
        {
            var history = GetById(id);
            if (history != null)
            {
                _context.Histories.Remove(history);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
