using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record EvaluationDTO(
        [Required]
        int PostulationId,

        [StringLength(1000, MinimumLength = 100)]
        string Justification
    );
}