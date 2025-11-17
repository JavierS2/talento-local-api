using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IOfferCategoryRepository
    {
        Task<IEnumerable<OfferCategory>> GetAllAsync();
        Task<OfferCategory?> GetByIdAsync(int id);
        Task AddAsync(OfferCategory offerCategory);
        Task UpdateAsync(OfferCategory offerCategory);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }

}

