using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Services.Interfaces;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Services
{
    public class ConvocationService : IConvocationService
    {
        private readonly IConvocationRepository _repo;

        public ConvocationService(IConvocationRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddConvocationAsync(Convocation convocation)
        {
            if (convocation == null) throw new System.ArgumentNullException(nameof(convocation));
            await _repo.AddAsync(convocation);
            await _repo.SaveAsync();
            return convocation.Id;
        }

        public async Task<IEnumerable<Convocation>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Convocation?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Convocation?> GetByStatusAsync(ConvocationStatus status)
        {
            return await _repo.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<Convocation>> SearchByLocationAsync(string location)
        {
            return await _repo.SearchByLocationAsync(location);
        }

        public async Task<IEnumerable<Convocation>> SearchByAvailablePlacesAsync(int minAvailablePlaces)
        {
            return await _repo.SearchByAvailablePlacesAsync(minAvailablePlaces);
        }

        public async Task<Convocation?> UpdateAsync(int id, Convocation convocation)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            // map fields (simple mapping)
            existing.Title = convocation.Title;
            existing.Description = convocation.Description;
            existing.Type = convocation.Type;
            existing.ClosingDate = convocation.ClosingDate;
            existing.State = convocation.State;
            existing.Location = convocation.Location;
            existing.AvailablePlaces = convocation.AvailablePlaces;
            existing.Requirements = convocation.Requirements;
            existing.UpdatedAt = System.DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();
            return existing;
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
