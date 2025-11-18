using Microsoft.EntityFrameworkCore;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _repository;
        private readonly DbDevopsContext _context;

        public EvaluationService(IEvaluationRepository repository, DbDevopsContext context)
        {
            _repository = repository;
            _context = context;
        }
        

        // Obtener todas las evaluaciones
        public async Task<List<EvaluationDTO>> GetAllAsync()
        {
            var evaluations = await _repository.GetAllAsync();

            return evaluations
                .Select(e => EvaluationMapper.ToDTO(e))
                .ToList();
        }

        // Obtener evaluación por ID
        public async Task<EvaluationDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la evaluación debe ser mayor a 0.");

            var evaluation = await _repository.GetByIdAsync(id);
            if (evaluation == null)
                throw new KeyNotFoundException($"No se encontró la evaluación con ID {id}.");

            return EvaluationMapper.ToDTO(evaluation);
        }

        // Crear nueva evaluación
        public async Task<EvaluationDTO> CreateAsync(EvaluationDTO evaluationDTO)
        {
            if (evaluationDTO == null)
                throw new ArgumentNullException(nameof(evaluationDTO));

            if (evaluationDTO.PostulationId <= 0)
                throw new ArgumentException("El campo 'PostulationId' es obligatorio y debe ser válido.");

            if (string.IsNullOrWhiteSpace(evaluationDTO.Justification))
                throw new ArgumentException("La justificación es obligatoria.");

            
            await ValidateForeignKeysAsync(evaluationDTO);

            var entity = EvaluationMapper.ToEntity(evaluationDTO);

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return EvaluationMapper.ToDTO(entity);
        }


        // Actualizar evaluación existente
        public async Task<bool> UpdateAsync(int id, EvaluationDTO evaluationDTO)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la evaluación debe ser mayor a 0.");

            if (evaluationDTO == null)
                throw new ArgumentNullException(nameof(evaluationDTO));

            if (string.IsNullOrWhiteSpace(evaluationDTO.Justification))
                throw new ArgumentException("La justificación es obligatoria.");

            
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la evaluación con ID {id}.");

            
            await ValidateForeignKeysAsync(evaluationDTO);

  
            existing.Justification = evaluationDTO.Justification;
            existing.PostulationId = evaluationDTO.PostulationId;
            existing.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }


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

        private async Task ValidateForeignKeysAsync(EvaluationDTO dto)
        {
            // Validar que la postulación exista
            bool postulationExists = await _context.Postulations
                .AnyAsync(p => p.Id == dto.PostulationId);

            if (!postulationExists)
                throw new ArgumentException($"La postulación con ID {dto.PostulationId} no existe.");

            // Validar que NO exista ya una evaluación para esta postulación
            bool alreadyEvaluated = await _context.Evaluations
                .AnyAsync(e => e.PostulationId == dto.PostulationId);

            if (alreadyEvaluated)
                throw new ArgumentException(
                    $"La postulación con ID {dto.PostulationId} ya tiene una evaluación registrada."
                );
        }




    }




}
