using TalentoLocal.Models;
using TalentoLocal.DTOs;
using System.Collections.Generic;

namespace TalentoLocal.Mappers
{
    public static class PostulationStatusMapper
    {
        public static PostulationStatusDTO ToDTO(PostulationStatus entity)
        {
            if (entity == null)
                return null!;

            return new PostulationStatusDTO(
                entity.Name ?? string.Empty
            );
        }

        public static PostulationStatus ToEntity(PostulationStatusDTO dto)
        {
            if (dto == null)
                return null!;

            return new PostulationStatus
            {
                Name = dto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static List<PostulationStatusDTO> ToDTOList(IEnumerable<PostulationStatus> entities)
        {
            var list = new List<PostulationStatusDTO>();
            foreach (var entity in entities)
            {
                list.Add(ToDTO(entity));
            }
            return list;
        }

        public static List<PostulationStatus> ToEntityList(IEnumerable<PostulationStatusDTO> dtos)
        {
            var list = new List<PostulationStatus>();
            foreach (var dto in dtos)
            {
                list.Add(ToEntity(dto));
            }
            return list;
        }
    }
}
