using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record OfferCategoryDTO(

        [param: Required(ErrorMessage = "El nombre es obligatorio.")]
        [param: StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        string Name
    );
}
