using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<FavoriteDTO>> GetAllAsync();
        Task<FavoriteDTO> CreateAsync(FavoriteDTO favoriteDTO);
        Task<FavoriteDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, FavoriteDTO favoriteDTO);
        Task<bool> DeleteAsync(int id);
        Task<List<FavoriteDTO>> GetByUserAsync(string userId);
        Task<bool> ToggleAsync(string userId, int offerId);
        // Toggle = si existe lo borra, si no existe lo agrega
    }
}
