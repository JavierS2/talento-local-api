using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record PostulationStatusDTO(
        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        string Name
    );
}
