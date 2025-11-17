using System.Linq;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

public class PostulationStatusService : IPostulationStatusService
{
    private readonly IPostulationStatusRepository _repository;

    public PostulationStatusService(IPostulationStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PostulationStatusDTO>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities?.Select(PostulationStatusMapper.ToDTO).ToList() ?? new List<PostulationStatusDTO>();
    }

    public async Task<PostulationStatusDTO> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del estado de postulación no puede ser menor o igual a cero.");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"No se encontró ningún estado de postulación con el ID {id}.");

        return PostulationStatusMapper.ToDTO(entity);
    }

    public async Task<PostulationStatusDTO> CreateAsync(PostulationStatusDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "El estado de postulación no puede ser nulo.");

        // Validación de campo obligatorio
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("El nombre del estado de postulación es obligatorio.");

        var entity = PostulationStatusMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        var saved = await _repository.GetByIdAsync(entity.Id);
        if (saved == null)
            throw new InvalidOperationException("Error al crear el estado de postulación: no se encontró después de guardar.");

        return PostulationStatusMapper.ToDTO(saved);
    }

    public async Task<bool> UpdateAsync(int id, PostulationStatusDTO dto)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del estado de postulación no puede ser menor o igual a cero.");

        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "El estado de postulación no puede ser nulo.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró ningún estado de postulación con el ID {id}.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("El nombre del estado de postulación es obligatorio.");

        var entity = PostulationStatusMapper.ToEntity(dto);
        entity.Id = id;

        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del estado de postulación no puede ser menor o igual a cero.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró ningún estado de postulación con el ID {id}.");

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }
}
