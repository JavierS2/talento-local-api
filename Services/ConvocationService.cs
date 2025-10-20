using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services
{
    public class ConvocationService : IConvocationService
    {
        private readonly DbDevopsContext _db;

        public ConvocationService(DbDevopsContext db)
        {
            _db = db;
        }

        public async Task<int> AddConvocationAsync(Convocation convocation)
        {
            if (convocation == null) throw new System.ArgumentNullException(nameof(convocation));
            _db.Convocations.Add(convocation);
            await _db.SaveChangesAsync();
            return convocation.Id;
        }

        public async Task<IEnumerable<Convocation>> GetAllAsync()
        {
            return await _db.Convocations
                .Include(c => c.PublishingEntity)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Convocation?> GetByIdAsync(int id)
        {
            return await _db.Convocations
                .Include(c => c.PublishingEntity)
                .Include(c => c.Offers)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Convocation?> GetByStatusAsync(ConvocationStatus status)
        {
            return await _db.Convocations
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.State == status);
        }

        public async Task<IEnumerable<Convocation>> SearchByLocationAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) return new List<Convocation>();
            var pattern = location.Trim().ToLower();
            return await _db.Convocations
                .Where(c => c.Location.ToLower().Contains(pattern))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Convocation>> SearchByAvailablePlacesAsync(int minAvailablePlaces)
        {
            return await _db.Convocations
                .Where(c => c.AvailablePlaces >= minAvailablePlaces)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Convocation?> UpdateAsync(int id, Convocation convocation)
        {
            var existing = await _db.Convocations.FindAsync(id);
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

            _db.Convocations.Update(existing);
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Convocations.FindAsync(id);
            if (existing == null) return false;
            _db.Convocations.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
