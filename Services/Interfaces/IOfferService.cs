using TalentoLocal.Models;
using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IOfferService
    {
        Task<List<OfferDTO>> GetAllAsync();
        Task<OfferDTO> CreateAsync(OfferDTO offerDTO);
        Task<OfferDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, OfferDTO offerDTO);
        Task<bool> DeleteAsync(int id);
    }
}
