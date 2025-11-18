using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IPostulationRepository
    {
        Task<IEnumerable<Postulation>> GetAllAsync();
        Task<Postulation?> GetByIdAsync(int id);
        Task AddAsync(Postulation postulation);
        Task UpdateAsync(Postulation postulation);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}

