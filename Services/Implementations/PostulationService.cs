using System.Linq;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

public class PostulationService : IPostulationService
{
    private readonly IPostulationRepository _repository;

    public PostulationService(IPostulationRepository repository)
    {
        _repository = repository;
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

        var entity = PostulationMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return PostulationMapper.ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, PostulationDTO dto)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la postulación no puede ser menor o igual a cero.");

        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "La postulación no puede ser nula.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró ninguna postulación con el ID {id}.");

        var entity = PostulationMapper.ToEntity(dto);
        entity.Id = id;

        await _repository.UpdateAsync(entity);
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
}
