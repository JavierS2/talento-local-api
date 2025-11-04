using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IOfferService
    {
        Task<int> AddOfferAsync(Offer offer);
        Task<IEnumerable<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Offer offer);
        Task<bool> DeleteAsync(int id);
    }
}
