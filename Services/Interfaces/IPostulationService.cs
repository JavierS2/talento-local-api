using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IPostulationService
    {
        Task<IEnumerable<PostulationDTO>> GetAllAsync();
        Task<PostulationDTO> GetByIdAsync(int id);
        Task<PostulationDTO> CreateAsync(PostulationDTO postulationDTO);
        Task<bool> UpdateAsync(int id, PostulationDTO postulationDTO);
        Task<bool> DeleteAsync(int id);
    }
}
