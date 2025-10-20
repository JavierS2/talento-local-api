using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<int> AddHistoryAsync(History history);
        Task<IEnumerable<History>> GetAllAsync();
        Task<History?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, History history);
    }
}
