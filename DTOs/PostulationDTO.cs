using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record PostulationDTO(
    [param: Required(ErrorMessage = "El usuario es obligatorio.")]
    int UserId,

    [param: Required(ErrorMessage = "La oferta es obligatoria.")]
    int OfferId,

    [param: Required(ErrorMessage = "El documento es obligatorio.")]
    int DocumentFile,

    int? StatusId,
    string? StatusName
);

}
