using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IConvocationRepository
    {
        Task<IEnumerable<Convocation>> GetAllAsync();
        Task<Convocation?> GetByIdAsync(int id);
        Task<Convocation?> GetByStatusAsync(ConvocationStatus status);
        Task<IEnumerable<Convocation>> SearchByLocationAsync(string location);
        Task<IEnumerable<Convocation>> SearchByAvailablePlacesAsync(int minAvailablePlaces);
        Task AddAsync(Convocation convocation);
        Task UpdateAsync(Convocation convocation);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
