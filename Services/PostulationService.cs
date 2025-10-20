using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services
{
    public class PostulationService : IPostulationService
    {
        private readonly DbDevopsContext _db;

        public PostulationService(DbDevopsContext db)
        {
            _db = db;
        }

        public async Task<int> AddPostulationAsync(Postulation postulation)
        {
            if (postulation == null) throw new System.ArgumentNullException(nameof(postulation));
            _db.Postulations.Add(postulation);
            await _db.SaveChangesAsync();
            return postulation.Id;
        }

        public async Task<IEnumerable<Postulation>> GetAllAsync()
        {
            return await _db.Postulations.AsNoTracking().ToListAsync();
        }

        public async Task<Postulation?> GetByIdAsync(int id)
        {
            return await _db.Postulations.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, Postulation postulation)
        {
            var existing = await _db.Postulations.FindAsync(id);
            if (existing == null) return false;
            // Map fields
            existing.AttachedDocument = postulation.AttachedDocument;
            existing.CompanyObservation = postulation.CompanyObservation;
            existing.ApplicationDate = postulation.ApplicationDate;
            existing.UpdatedAt = System.DateTime.UtcNow;

            _db.Postulations.Update(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Postulations.FindAsync(id);
            if (existing == null) return false;
            _db.Postulations.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
