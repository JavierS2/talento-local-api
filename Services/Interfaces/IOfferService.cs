using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllAsync();
        Task<Offer> CreateAsync(Offer offer);
        Task<Offer?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Offer offer);
        Task<bool> DeleteAsync(int id);
    }
}
