using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IPostulationService
    {
        Task<int> AddPostulationAsync(Postulation postulation);
        Task<IEnumerable<Postulation>> GetAllAsync();
        Task<Postulation?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Postulation postulation);
        Task<bool> DeleteAsync(int id);
    }
}
