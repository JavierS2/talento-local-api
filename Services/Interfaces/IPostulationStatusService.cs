using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
    public interface IPostulationStatusService
    {
        Task<IEnumerable<PostulationStatus>> GetAllAsync();
        Task<PostulationStatus> CreateAsync(PostulationStatus postulationStatus);
        Task<PostulationStatus?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, PostulationStatus postulationStatus);
        Task<bool> DeleteAsync(int id);
    }
}
