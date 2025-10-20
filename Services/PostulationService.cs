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

            var entity = new Postulacion
            {
                CreateTime = postulation.ApplicationDate,
                Titulo = postulation.AttachedDocument,
                Descripcion = postulation.CompanyObservation
            };

            _db.Postulaciones.Add(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Postulation>> GetAllAsync()
        {
            var list = await _db.Postulaciones.AsNoTracking().ToListAsync();
            var result = new List<Postulation>();
            foreach (var e in list)
            {
                result.Add(new Postulation
                {
                    Id = e.Id,
                    ApplicationDate = e.CreateTime ?? System.DateTime.MinValue,
                    AttachedDocument = e.Titulo ?? string.Empty,
                    CompanyObservation = e.Descripcion ?? string.Empty
                });
            }
            return result;
        }

        public async Task<Postulation?> GetByIdAsync(int id)
        {
            var e = await _db.Postulaciones.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (e == null) return null;
            return new Postulation
            {
                Id = e.Id,
                ApplicationDate = e.CreateTime ?? System.DateTime.MinValue,
                AttachedDocument = e.Titulo ?? string.Empty,
                CompanyObservation = e.Descripcion ?? string.Empty
            };
        }

        public async Task<bool> UpdateAsync(int id, Postulation postulation)
        {
            var existing = await _db.Postulaciones.FindAsync(id);
            if (existing == null) return false;

            existing.Titulo = postulation.AttachedDocument;
            existing.Descripcion = postulation.CompanyObservation;
            existing.CreateTime = postulation.ApplicationDate;

            _db.Postulaciones.Update(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Postulaciones.FindAsync(id);
            if (existing == null) return false;
            _db.Postulaciones.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
