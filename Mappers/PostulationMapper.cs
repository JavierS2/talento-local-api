using TalentoLocal.DTOs;
using TalentoLocal.Models;
namespace TalentoLocal.Mappers
{
    public static class PostulationMapper
    {
        // 🔹 Convierte de entidad → DTO
        public static PostulationDTO ToDto(Postulation postulation)
        {
            if (postulation == null)
                throw new ArgumentNullException(nameof(postulation));

            return new PostulationDTO
            (
                postulation.UserId,
                postulation.OfferId,
                postulation.DocumentFile,
                postulation.StatusId,
                postulation.Status?.Name // obtiene el nombre del estado si existe
            );
        }

        // 🔹 Convierte de DTO → entidad
        public static Postulation ToEntity(PostulationDTO dto)
        {
            return new Postulation
            {
                UserId = dto.UserId,
                OfferId = dto.OfferId,
                DocumentFile = dto.DocumentFile,
                StatusId = dto.StatusId ?? 0,
            };
        }
    }
}
