using TalentoLocal.Models;
using TalentoLocal.DTOs;

namespace TalentoLocal.Mappers
{
    public static class FavoriteMapper
    {
        public static FavoriteDTO ToDTO(Favorite entity)
        {
            if (entity == null)
                return null!;

            return new FavoriteDTO(
                entity.UserId,
                entity.OfferId
            );
        }

        public static Favorite ToEntity(FavoriteDTO dto)
        {
            if (dto == null)
                return null!;

            return new Favorite
            {
                UserId = dto.UserId,
                OfferId = dto.OfferId,
                CreatedAt = DateTime.Now
            };
        }
    }
}
