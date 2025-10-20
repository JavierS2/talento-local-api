using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _repo;

        public HistoryService(IHistoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddHistoryAsync(History history)
        {
            if (history == null) throw new System.ArgumentNullException(nameof(history));
            await _repo.AddAsync(history);
            await _repo.SaveAsync();
            return history.Id;
        }

        public async Task<IEnumerable<History>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<History?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, History history)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.EventType = history.EventType;
            existing.ResponsibleUserId = history.ResponsibleUserId;
            existing.ReferenceEntity = history.ReferenceEntity;
            existing.ReferenceId = history.ReferenceId;
            existing.EventDescription = history.EventDescription;
            existing.EventDate = history.EventDate;
            existing.PreviousState = history.PreviousState;
            existing.NewState = history.NewState;
            existing.Observations = history.Observations;

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
