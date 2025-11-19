using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetAllAsync();
        Task<Favorite?> GetByIdAsync(int id);
        Task AddAsync(Favorite favorite);
        Task UpdateAsync(Favorite favorite);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
