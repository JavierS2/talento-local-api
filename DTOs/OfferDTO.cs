using System.ComponentModel.DataAnnotations;

namespace TalentoLocal.DTOs
{
    public record OfferDTO(
        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Title,

        string? SubTitle,

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        string Description,

        [Required]
        string Modality,

        [Required]
        [Range(0, int.MaxValue)]
        int Salary,

        [Required]
        [StringLength(1000)]
        string Requeriments,

        [Required]
        [StringLength(1000)]
        string Benefits,

        [Required]
        [Range(0, 30)]
        int YearsExperience,

        [Required]
        [StringLength(200)]
        string Location,

        [Required]
        string Journey,

        string? Schedule,

        [Required]
        [Range(1, 1000)]
        int AvailablePlaces,

        [Required]
        string Status,

        [Required]
        string ContractType,

        [Required]
        string PaymentType,

        [Required]
        DateTime PublicationDate,

        DateTime? ClosingDate,

        [Required]
        int CompanyId,

        [Required]
        int CategoryId
    );
}
