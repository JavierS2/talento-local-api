using TalentoLocal.DTOs;
using TalentoLocal.Models;

namespace TalentoLocal.Mappers
{
    public static class OfferMapper
    {
        public static OfferResponseDTO ToDTO(Offer entity)
        {
            if (entity == null)
                return null!;

            return new OfferResponseDTO(
                entity.Id,
                entity.Title,
                entity.SubTitle,
                entity.Description,
                entity.Modality,
                entity.Salary,
                entity.Requeriments,
                entity.Benefits,
                entity.YearsExperience,
                entity.Location,
                entity.Journey,
                entity.Schedule,
                entity.AvailablePlaces,
                entity.Status,
                entity.ContractType,
                entity.PaymentType,
                entity.PublicationDate,
                entity.ClosingDate,
                entity.CompanyId,
                entity.CategoryId,
                entity.Featured,
                entity.Urgent,
                entity.Rating,
                entity.Category?.Name ?? "Sin categoría"
            );
        }


        public static Offer ToEntity(OfferDTO dto)
        {
            if (dto == null)
                return null!;

            return new Offer
            {
                Title = dto.Title,
                SubTitle = dto.SubTitle,
                Description = dto.Description,
                Modality = dto.Modality,
                Salary = dto.Salary,
                Requeriments = dto.Requeriments,
                Benefits = dto.Benefits,
                YearsExperience = dto.YearsExperience,
                Location = dto.Location,
                Journey = dto.Journey,
                Schedule = dto.Schedule,
                AvailablePlaces = dto.AvailablePlaces,
                Status = dto.Status,
                ContractType = dto.ContractType,
                PaymentType = dto.PaymentType,
                PublicationDate = dto.PublicationDate,
                ClosingDate = dto.ClosingDate,
                CompanyId = dto.CompanyId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static List<OfferResponseDTO> ToDTOList(IEnumerable<Offer> entities)
        {
            var list = new List<OfferResponseDTO>();
            foreach (var entity in entities)
            {
                list.Add(ToDTO(entity));
            }
            return list;
        }

        public static OfferDetailDTO ToDetailDTO(Offer entity)
        {
            if (entity == null)
                return null!;

            // Calcular el tiempo publicado, ej: "hace 3 días"
            string postedTime = (DateTime.Now - entity.PublicationDate).Days switch
            {
                0 => "Hoy",
                1 => "Hace 1 día",
                int n when n < 7 => $"Hace {n} días",
                _ => $"{entity.PublicationDate:dd/MM/yyyy}"
            };

            return new OfferDetailDTO(
                entity.Id,
                entity.Title,
                entity.SubTitle,
                "Empresa desconocida", //entity.Company?.Name ?? 
                entity.Location,
                entity.ContractType,
                entity.Schedule,
                entity.Modality,
                entity.Salary,
                entity.PaymentType,
                postedTime,
                entity.PublicationDate, //
                entity.Featured ?? false, //
                entity.Urgent ?? false, //
                entity.Rating,          
                entity.Category?.Name ?? "Sin categoría",
                entity.YearsExperience,
                entity.Journey,
                entity.Description,
                entity.Requeriments,
                entity.Benefits
            );
        }




        public static List<Offer> ToEntityList(IEnumerable<OfferDTO> dtos)
        {
            var list = new List<Offer>();
            foreach (var dto in dtos)
            {
                list.Add(ToEntity(dto));
            }
            return list;
        }
    }
}
