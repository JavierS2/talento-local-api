using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TalentoLocal.DTOs
{
    public record PostulationDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "La oferta es obligatoria.")]
        public int OfferId { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        public IFormFile DocumentFile { get; set; } = default!;

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }

    public class PostulationResponseDTO
    {
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public string DocumentFileUrl { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }

}
