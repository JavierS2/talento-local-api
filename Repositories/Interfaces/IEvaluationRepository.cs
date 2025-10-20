using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
	public interface IEvaluationRepository
	{
		Task<IEnumerable<Evaluation>> GetAllAsync();
		Task<Evaluation?> GetByIdAsync(int id);
		Task AddAsync(Evaluation evaluation);
		Task UpdateAsync(Evaluation evaluation);
		Task DeleteAsync(int id);
		Task SaveAsync();
	}
}
