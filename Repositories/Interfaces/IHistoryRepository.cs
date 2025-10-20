using System.Collections.Generic;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IHistoryRepository
    {
        IEnumerable<History> GetAll();
        History? GetById(int id);
        void Add(History history);
        void Update(History history);
        void Delete(int id);
        void Save();
    }
}
