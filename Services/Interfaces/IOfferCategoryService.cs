using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IOfferCategoryService
    {
        Task<List<OfferCategoryDTO>> GetAllAsync();
        Task<OfferCategoryDTO> GetByIdAsync(int id);
        Task<OfferCategoryDTO> CreateAsync(OfferCategoryDTO dto);
        Task<bool> UpdateAsync(int id, OfferCategoryDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
