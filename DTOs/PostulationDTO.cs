using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TalentoLocal.DTOs
{
    public record PostulationDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "La oferta es obligatoria.")]
        public int OfferId { get; set; }

        public IFormFile? DocumentFile { get; set; }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }

    public class PostulationResponseDTO
    {
        public int PostulationId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int OfferId { get; set; }
        public string? DocumentFileUrl { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }

}
