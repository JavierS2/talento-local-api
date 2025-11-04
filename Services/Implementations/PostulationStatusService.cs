using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class PostulationStatusService : IPostulationStatusService
    {
        private readonly IPostulationStatusRepository _postulationStatusRepository;

        public PostulationStatusService(IPostulationStatusRepository postulationStatusRepository)
        {
            _postulationStatusRepository = postulationStatusRepository;
        }

        public async Task<List<PostulationStatus>> GetAllAsync()
        {
            return await _postulationStatusRepository.GetAllAsync();
        }

        public async Task<PostulationStatus?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _postulationStatusRepository.GetByIdAsync(id);
        }

        public async Task<PostulationStatus> CreateAsync(PostulationStatus postulationStatus)
        {
            if (postulationStatus == null)
                throw new ArgumentNullException(nameof(postulationStatus), "El estado de postulación no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(postulationStatus.Name))
                throw new ArgumentException("El nombre del estado es obligatorio.");

            postulationStatus.CreatedAt = DateTime.UtcNow;
            postulationStatus.UpdatedAt = DateTime.UtcNow;

            await _postulationStatusRepository.AddAsync(postulationStatus);
            await _postulationStatusRepository.SaveChangesAsync();

            return postulationStatus;
        }

        public async Task<bool> UpdateAsync(int id, PostulationStatus postulationStatus)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var existing = await _postulationStatusRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            // Actualiza solo los campos editables
            existing.Name = postulationStatus.Name ?? existing.Name;
            existing.UpdatedAt = DateTime.UtcNow;

            await _postulationStatusRepository.UpdateAsync(existing);
            await _postulationStatusRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var existing = await _postulationStatusRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _postulationStatusRepository.DeleteAsync(id);
            await _postulationStatusRepository.SaveChangesAsync();

            return true;
        }
    }
}
