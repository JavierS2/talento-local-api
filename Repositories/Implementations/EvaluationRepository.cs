using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly DbDevopsContext _context;

        public EvaluationRepository(DbDevopsContext context)
        {
            _context = context;
        }

        
        public async Task<List<Evaluation>> GetAllAsync()
        {
            return await _context.Evaluations
                .Include(e => e.Postulation)
                .ToListAsync();
        }

        public async Task<Evaluation?> GetByIdAsync(int id)
        {
            return await _context.Evaluations
                .Include(e => e.Postulation)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Evaluation evaluation)
        {
            await _context.Evaluations.AddAsync(evaluation);
        }

        public Task UpdateAsync(Evaluation evaluation)
        {
            _context.Evaluations.Update(evaluation);
            return Task.CompletedTask;
        }

        // 🔹 Eliminar una evaluación por ID
        public async Task DeleteAsync(int id)
        {
            var evaluation = await GetByIdAsync(id);
            if (evaluation != null)
            {
                _context.Evaluations.Remove(evaluation);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
