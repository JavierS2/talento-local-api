using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Services.Interfaces;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Services
{
	public class EvaluationService : IEvaluationService
	{
		private readonly IEvaluationRepository _repo;

		public EvaluationService(IEvaluationRepository repo)
		{
			_repo = repo;
		}

		public async Task<int> AddEvaluationAsync(Evaluation evaluation)
		{
			if (evaluation == null) throw new System.ArgumentNullException(nameof(evaluation));
			await _repo.AddAsync(evaluation);
			await _repo.SaveAsync();
			return evaluation.Id;
		}

		public async Task<IEnumerable<Evaluation>> GetAllAsync()
		{
			return await _repo.GetAllAsync();
		}

		public async Task<Evaluation?> GetByStatusAsync(EvaluationStatus status)
		{
			var all = await _repo.GetAllAsync();
			foreach (var e in all)
			{
				if (e.Status == status) return e;
			}
			return null;
		}

		public async Task<bool> UpdateAsync(int id, Evaluation evaluation)
		{
			var existing = await _repo.GetByIdAsync(id);
			if (existing == null) return false;

			// Map fields (partial mapping; extend as needed)
			existing.Criteria = evaluation.Criteria;
			existing.Result = evaluation.Result;
			existing.Comments = evaluation.Comments;
			existing.EvaluationDate = evaluation.EvaluationDate;
			existing.Status = evaluation.Status;
			existing.UpdatedAt = System.DateTime.UtcNow;

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
