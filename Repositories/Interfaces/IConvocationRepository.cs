using System.Collections.Generic;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IConvocationRepository
    {
        IEnumerable<Convocation> GetAll();
        Convocation? GetById(int id);
        void Add(Convocation convocation);
        void Update(Convocation convocation);
        void Delete(int id);
        void Save();
    }
}
