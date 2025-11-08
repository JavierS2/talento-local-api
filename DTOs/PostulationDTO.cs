using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record PostulationDTO(
        [property: Required(ErrorMessage = "El usuario es obligatorio.")]
        int UserId,

        [property: Required(ErrorMessage = "La oferta es obligatoria.")]
        int OfferId,

        [property: Required(ErrorMessage = "El documento es obligatorio.")]
        int DocumentFile,

        int? StatusId,
        string? StatusName
    );
}
