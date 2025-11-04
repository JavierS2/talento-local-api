using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class PostulationService : IPostulationService
    {
        private readonly IPostulationRepository _repo;

        public PostulationService(IPostulationRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddPostulationAsync(Postulation postulation)
        {
            if (postulation == null) throw new ArgumentNullException(nameof(postulation));
            await _repo.AddAsync(postulation);
            await _repo.SaveAsync();
            return postulation.Id;
        }

        public async Task<IEnumerable<Postulation>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Postulation?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Postulation postulation)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            // Map fields
            existing.AttachedDocument = postulation.AttachedDocument;
            existing.CompanyObservation = postulation.CompanyObservation;
            existing.ApplicationDate = postulation.ApplicationDate;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
            return true;
        }
    }
}
