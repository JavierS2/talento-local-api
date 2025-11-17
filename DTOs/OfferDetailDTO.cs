namespace TalentoLocal.DTOs
{
    public record OfferDetailDTO(
        int Id,
        string Title,
        string? SubTitle,
        string Company,
        string Location,
        string Type,
        string Schedule,
        string Modality,
        decimal Salary,
        string PaymentType,
        string PostedTime,
        DateTime PostedDate,
        bool Featured,
        bool Urgent,
        double? Rating,
        string Category,
        int YearsExperience,
        string Journey,
        string Description,
        string Requeriments,
        string? Benefits
    );
}
