using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly DbDevopsContext _db;

        public HistoryService(DbDevopsContext db)
        {
            _db = db;
        }

        public async Task<int> AddHistoryAsync(History history)
        {
            if (history == null) throw new System.ArgumentNullException(nameof(history));
            _db.Histories.Add(history);
            await _db.SaveChangesAsync();
            return history.Id;
        }

        public async Task<IEnumerable<History>> GetAllAsync()
        {
            return await _db.Histories.AsNoTracking().ToListAsync();
        }

        public async Task<History?> GetByIdAsync(int id)
        {
            return await _db.Histories.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, History history)
        {
            var existing = await _db.Histories.FindAsync(id);
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

            _db.Histories.Update(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Histories.FindAsync(id);
            if (existing == null) return false;
            _db.Histories.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
