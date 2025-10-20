using System.Collections.Generic;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IPostulationRepository
    {
        IEnumerable<Postulation> GetAll();
        Postulation? GetById(int id);
        void Add(Postulation postulation);
        void Update(Postulation postulation);
        void Delete(int id);
        void Save();
    }
}
