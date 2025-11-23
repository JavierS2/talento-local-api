using TalentoLocal.Models;
using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IOfferService
    {
        Task<List<OfferResponseDTO>> GetAllAsync();
        Task<OfferResponseDTO> CreateAsync(OfferDTO offerDTO);
        Task<OfferResponseDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, OfferDTO offerDTO);
        Task<bool> DeleteAsync(int id);
        Task<List<OfferResponseDTO>> GetOffersByUserIdAsync(string userId);

    }
}
