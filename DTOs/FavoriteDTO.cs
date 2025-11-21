using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record FavoriteDTO(
        [Required]
        int UserId,

        [Required]
        int OfferId
    );
}
