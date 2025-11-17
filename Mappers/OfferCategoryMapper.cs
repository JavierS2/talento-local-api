using System;
using TalentoLocal.DTOs;
using TalentoLocal.Models;

namespace TalentoLocal.Mappers
{
    public static class OfferCategoryMapper
    {
        // 🔹 Convierte de entidad → DTO
        public static OfferCategoryDTO ToDto(OfferCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            return new OfferCategoryDTO
            (
                Name: category.Name ?? string.Empty
            );
        }

        // 🔹 Convierte de DTO → entidad
        public static OfferCategory ToEntity(OfferCategoryDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new OfferCategory
            {
                Name = dto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
