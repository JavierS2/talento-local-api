using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _repository;

        public EvaluationService(IEvaluationRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las evaluaciones
        public async Task<List<Evaluation>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Obtener evaluación por ID
        public async Task<Evaluation?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la evaluación debe ser mayor a 0.");

            var evaluation = await _repository.GetByIdAsync(id);
            if (evaluation == null)
                throw new KeyNotFoundException($"No se encontró la evaluación con ID {id}.");

            return evaluation;
        }

        // Crear nueva evaluación
        public async Task<Evaluation> CreateAsync(Evaluation evaluation)
        {
            if (evaluation == null)
                throw new ArgumentNullException(nameof(evaluation));

            if (evaluation.PostulationId <= 0)
                throw new ArgumentException("El campo 'PostulationId' es obligatorio y debe ser válido.");

            if (string.IsNullOrWhiteSpace(evaluation.Justification))
                throw new ArgumentException("La justificación es obligatoria.");

            evaluation.CreatedAt = DateTime.Now;
            evaluation.UpdatedAt = DateTime.Now;

            await _repository.AddAsync(evaluation);
            await _repository.SaveChangesAsync();

            return evaluation;
        }

        // Actualizar evaluación existente
        public async Task<bool> UpdateAsync(int id, Evaluation evaluation)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la evaluación debe ser mayor a 0.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la evaluación con ID {id}.");

            if (string.IsNullOrWhiteSpace(evaluation.Justification))
                throw new ArgumentException("La justificación es obligatoria.");

            // Actualizamos los campos permitidos
            existing.Justification = evaluation.Justification;
            existing.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }

        // Eliminar evaluación
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la evaluación con ID {id}.");

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
