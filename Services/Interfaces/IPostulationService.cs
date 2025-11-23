using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IPostulationService
    {
        Task<IEnumerable<PostulationResponseDTO>> GetAllAsync();
        Task<PostulationResponseDTO> GetByIdAsync(int id);
        Task<PostulationResponseDTO> CreateAsync(PostulationDTO postulationDTO);
        Task<bool> UpdateAsync(int id, PostulationDTO postulationDTO);
        Task<bool> DeleteAsync(int id);
        Task<List<PostulationResponseDTO>> GetPostulationsByUserIdAsync(string userId);

    }
}
