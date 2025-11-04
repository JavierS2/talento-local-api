using TalentoLocal.Models;

namespace TalentoLocal.Services.Interfaces
{
	public interface IEvaluationService
	{
		Task<IEnumerable<Evaluation>> GetAllAsync();
		Task<Evaluation> CreateAsync(Evaluation evaluation);
        Task<Evaluation?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Evaluation evaluation);
		Task<bool> DeleteAsync(int id);
	}
}
