using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;

namespace TalentoLocal.Services.Interfaces
{
    public interface IConvocationService
    {
        Task<int> AddConvocationAsync(Convocation convocation);
        Task<IEnumerable<Convocation>> GetAllAsync();
        Task<Convocation?> GetByIdAsync(int id);
        Task<Convocation?> GetByStatusAsync(ConvocationStatus status);
        Task<IEnumerable<Convocation>> SearchByLocationAsync(string location);
        Task<IEnumerable<Convocation>> SearchByAvailablePlacesAsync(int minAvailablePlaces);
        Task<Convocation?> UpdateAsync(int id, Convocation convocation);
    }
}
