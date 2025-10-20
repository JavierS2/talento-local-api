using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
        Task AddAsync(Offer offer);
        Task UpdateAsync(Offer offer);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
