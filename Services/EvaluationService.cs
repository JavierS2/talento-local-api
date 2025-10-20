using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services
{
	public class EvaluationService : IEvaluationService
	{
		private readonly DbDevopsContext _db;

		public EvaluationService(DbDevopsContext db)
		{
			_db = db;
		}

		public async Task<int> AddEvaluationAsync(Evaluation evaluation)
		{
			if (evaluation == null) throw new System.ArgumentNullException(nameof(evaluation));
			_db.Evaluations.Add(evaluation);
			await _db.SaveChangesAsync();
			return evaluation.Id;
		}

		public async Task<IEnumerable<Evaluation>> GetAllAsync()
		{
			return await _db.Evaluations
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Evaluation?> GetByStatusAsync(EvaluationStatus status)
		{
			return await _db.Evaluations
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Status == status);
		}

		public async Task<bool> UpdateAsync(int id, Evaluation evaluation)
		{
			var existing = await _db.Evaluations.FindAsync(id);
			if (existing == null) return false;

			// Map fields (partial mapping; extend as needed)
			existing.Criteria = evaluation.Criteria;
			existing.Result = evaluation.Result;
			existing.Comments = evaluation.Comments;
			existing.EvaluationDate = evaluation.EvaluationDate;
			existing.Status = evaluation.Status;
			existing.UpdatedAt = System.DateTime.UtcNow;

			_db.Evaluations.Update(existing);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var existing = await _db.Evaluations.FindAsync(id);
			if (existing == null) return false;
			_db.Evaluations.Remove(existing);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}
