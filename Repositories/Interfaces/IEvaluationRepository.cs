using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
	public interface IEvaluationRepository
	{
		Task<List<Evaluation>> GetAllAsync();
		Task<Evaluation?> GetByIdAsync(int id);
		Task AddAsync(Evaluation evaluation);
		Task UpdateAsync(Evaluation evaluation);
		Task DeleteAsync(int id);
		Task SaveChangesAsync();
	}
}
