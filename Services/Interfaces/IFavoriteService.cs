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
        Task<List<FavoriteDTO>> GetByUserAsync(int userId);
        Task<bool> ToggleAsync(int userId, int offerId);
        // Toggle = si existe lo borra, si no existe lo agrega
    }
}
