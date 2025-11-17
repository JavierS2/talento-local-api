using TalentoLocal.Models;
using TalentoLocal.DTOs;

namespace TalentoLocal.Services.Interfaces
{
	public interface IEvaluationService
	{
		Task<List<EvaluationDTO>> GetAllAsync();
		Task<EvaluationDTO> CreateAsync(EvaluationDTO evaluationDTO);
        Task<EvaluationDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, EvaluationDTO evaluationDTO);
		Task<bool> DeleteAsync(int id);
	}
}
