using System.Collections.Generic;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
	public interface IEvaluationRepository
	{
		IEnumerable<Evaluation> GetAll();
		Evaluation? GetById(int id);
		void Add(Evaluation evaluation);
		void Update(Evaluation evaluation);
		void Delete(int id);
		void Save();
	}
}
