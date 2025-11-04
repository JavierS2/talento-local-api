using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class PostulationService : IPostulationService
    {
        private readonly IPostulationRepository _postulationRepository;

        public PostulationService(IPostulationRepository postulationRepository)
        {
            _postulationRepository = postulationRepository;
        }

        public async Task<IEnumerable<Postulation>> GetAllAsync()
        {
            return await _postulationRepository.GetAllAsync();
        }

        public async Task<Postulation?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _postulationRepository.GetByIdAsync(id);
        }

        public async Task<Postulation> CreateAsync(Postulation postulation)
        {
            if (postulation == null)
                throw new ArgumentNullException(nameof(postulation), "La postulación no puede ser nula.");

            if (postulation.UserId <= 0)
                throw new ArgumentException("El ID del usuario es inválido.");

            if (postulation.OfferId <= 0)
                throw new ArgumentException("El ID de la oferta es inválido.");

            postulation.CreatedAt = DateTime.UtcNow;
            postulation.UpdatedAt = DateTime.UtcNow;

            await _postulationRepository.AddAsync(postulation);
            await _postulationRepository.SaveChangesAsync();

            return postulation;
        }

        public async Task<bool> UpdateAsync(int id, Postulation postulation)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var existing = await _postulationRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            // Actualiza solo los campos permitidos
            existing.UserId = postulation.UserId;
            existing.OfferId = postulation.OfferId;
            existing.DocumentFile = postulation.DocumentFile;
            existing.StatusId = postulation.StatusId;
            existing.UpdatedAt = DateTime.UtcNow;

            await _postulationRepository.UpdateAsync(existing);
            await _postulationRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var existing = await _postulationRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _postulationRepository.DeleteAsync(id);
            await _postulationRepository.SaveChangesAsync();

            return true;
        }
    }
}
