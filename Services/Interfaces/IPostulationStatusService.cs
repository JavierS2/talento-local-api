using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
    public interface IPostulationStatusService
    {
        Task<List<PostulationStatusDTO>> GetAllAsync();
        Task<PostulationStatusDTO> CreateAsync(PostulationStatusDTO postulationStatusDTO);
        Task<PostulationStatusDTO> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, PostulationStatusDTO postulationStatusDTO);
        Task<bool> DeleteAsync(int id);
    }
}
