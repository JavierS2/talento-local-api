using TalentoLocal.DTOs;
using TalentoLocal.Models;
namespace TalentoLocal.Mappers
{
    public static class PostulationMapper
    {
        // 🔹 Convierte de entidad → DTO
        public static PostulationResponseDTO ToDto(Postulation postulation)
        {
            if (postulation == null)
                throw new ArgumentNullException(nameof(postulation));

            return new PostulationResponseDTO
            {
                UserId = postulation.UserId,
                OfferId = postulation.OfferId,
                DocumentFileUrl = postulation.DocumentFile,
                StatusId = postulation.StatusId,
                StatusName = postulation.Status?.Name
            };
        }


        // 🔹 Convierte de DTO → entidad
        public static Postulation ToEntity(PostulationDTO dto, string documentUrl)
        {
            return new Postulation
            {
                UserId = dto.UserId,
                OfferId = dto.OfferId,
                DocumentFile = documentUrl,
                StatusId = dto.StatusId
            };
        }
    }
}
