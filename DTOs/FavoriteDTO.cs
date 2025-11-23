using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record FavoriteDTO(
        [Required]
        string UserId,

        [Required]
        int OfferId
    );
}
