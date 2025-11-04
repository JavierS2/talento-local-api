using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IOfferCategoryService
    {
        Task<IEnumerable<OfferCategory>> GetAllAsync();
        Task<OfferCategory> CreateAsync(OfferCategory offerCategory);
        Task<OfferCategory?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, OfferCategory offerCategory);
        Task<bool> DeleteAsync(int id);
    }
}
