using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IPostulationStatusRepository
    {
        Task<IEnumerable<PostulationStatus>> GetAllAsync();
        Task<PostulationStatus?> GetByIdAsync(int id);
        Task AddAsync(PostulationStatus postulationStatus);
        Task UpdateAsync(PostulationStatus postulationStatus);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
