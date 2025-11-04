using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
	public interface IEvaluationService
	{
		Task<int> AddEvaluationAsync(Evaluation evaluation);
		Task<IEnumerable<Evaluation>> GetAllAsync();
		Task<bool> UpdateAsync(int id, Evaluation evaluation);
		Task<bool> DeleteAsync(int id);
	}
}
