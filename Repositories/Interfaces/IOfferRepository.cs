using System.Collections.Generic;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetAll();
        Offer? GetById(int id);
        void Add(Offer offer);
        void Update(Offer offer);
        void Delete(int id);
        void Save();
    }
}
