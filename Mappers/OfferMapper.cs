using TalentoLocal.DTOs;
using TalentoLocal.Models;

namespace TalentoLocal.Mappers
{
    public static class OfferMapper
    {
        public static OfferDTO ToDTO(Offer entity)
        {
            if (entity == null)
                return null!;

            return new OfferDTO(
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
                entity.CategoryId
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

        public static List<OfferDTO> ToDTOList(IEnumerable<Offer> entities)
        {
            var list = new List<OfferDTO>();
            foreach (var entity in entities)
            {
                list.Add(ToDTO(entity));
            }
            return list;
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
