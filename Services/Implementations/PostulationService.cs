using Microsoft.EntityFrameworkCore;
using System.Linq;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

public class PostulationService : IPostulationService
{
    private readonly IPostulationRepository _repository;
    private readonly DbDevopsContext _context;

    public PostulationService(IPostulationRepository repository, DbDevopsContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<PostulationDTO>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities?.Select(PostulationMapper.ToDto).ToList() ?? new List<PostulationDTO>();
    }

    public async Task<PostulationDTO> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la postulación no puede ser menor o igual a cero.");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"No se encontró ninguna postulación con el ID {id}.");

        return PostulationMapper.ToDto(entity);
    }

    public async Task<PostulationDTO> CreateAsync(PostulationDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "La postulación no puede ser nula.");


        await ValidateForeignKeysAsync(dto);

        var entity = PostulationMapper.ToEntity(dto);


        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return PostulationMapper.ToDto(entity);
    }


    public async Task<bool> UpdateAsync(int id, PostulationDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        await ValidateForeignKeysAsync(dto);

        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            throw new KeyNotFoundException($"No existe la postulación con ID {id}.");

        // 3. Actualizar solo los campos que sí pueden cambiar
        existing.UserId = dto.UserId;
        existing.OfferId = dto.OfferId;
        existing.DocumentFile = dto.DocumentFile;
        existing.StatusId = dto.StatusId;
        existing.UpdatedAt = DateTime.Now;

        // 4. Guardar
        await _repository.UpdateAsync(existing);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la postulación no puede ser menor o igual a cero.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró ninguna postulación con el ID {id}.");

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }

    private async Task ValidateForeignKeysAsync(PostulationDTO dto)
    {
        // Validar estado de la postulación
        bool statusExists = await _context.PostulationStatus
            .AnyAsync(p => p.Id == dto.StatusId);

        if (!statusExists)
            throw new ArgumentException(
                $"El estado de la postulación con ID {dto.StatusId} no existe."
            );

        // Validar oferta
        bool offerExists = await _context.Offers
            .AnyAsync(o => o.Id == dto.OfferId);

        if (!offerExists)
            throw new ArgumentException(
                $"La oferta con ID {dto.OfferId} no existe."
            );
    }
}
