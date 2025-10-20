using System.Collections.Generic;
using System.Linq;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly DbDevopsContext _context;

        public OfferRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public IEnumerable<Offer> GetAll()
        {
            return _context.Offers.ToList();
        }

        public Offer? GetById(int id)
        {
            return _context.Offers.FirstOrDefault(o => o.Id == id);
        }

        public void Add(Offer offer)
        {
            _context.Offers.Add(offer);
        }

        public void Update(Offer offer)
        {
            _context.Offers.Update(offer);
        }

        public void Delete(int id)
        {
            var offer = GetById(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
