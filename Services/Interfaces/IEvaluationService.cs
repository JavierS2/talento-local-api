using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;

namespace TalentoLocal.Services.Interfaces
{
	public interface IEvaluationService
	{
		Task<int> AddEvaluationAsync(Evaluation evaluation);
		Task<IEnumerable<Evaluation>> GetAllAsync();
		Task<Evaluation?> GetByStatusAsync(EvaluationStatus status);
		Task<bool> UpdateAsync(int id, Evaluation evaluation);
		Task<bool> DeleteAsync(int id);
	}
}
