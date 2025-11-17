using TalentoLocal.Models;
using TalentoLocal.DTOs;

namespace TalentoLocal.Mappers
{
    public static class EvaluationMapper
    {
        public static EvaluationDTO ToDTO(Evaluation entity)
        {
            if (entity == null)
                return null!;

            return new EvaluationDTO(
                entity.PostulationId,
                entity.Justification
            );
        }

        public static Evaluation ToEntity(EvaluationDTO dto)
        {
            if (dto == null)
                return null!;

            return new Evaluation
            {
                PostulationId = dto.PostulationId,
                Justification = dto.Justification,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
