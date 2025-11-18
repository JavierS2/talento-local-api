using Microsoft.EntityFrameworkCore;
using System.Linq;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Implementations;
using TalentoLocal.Services.Interfaces;

public class PostulationService : IPostulationService
{
    private readonly IPostulationRepository _repository;
    private readonly DbDevopsContext _context;
    private readonly IBlobStorageService _blobStorageService;

    public PostulationService(
        IPostulationRepository repository,
        DbDevopsContext context,
        IBlobStorageService blobStorageService)
    {
        _repository = repository;
        _context = context;
        _blobStorageService = blobStorageService;
    }

    public async Task<IEnumerable<PostulationResponseDTO>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities?.Select(PostulationMapper.ToDto).ToList() ?? new List<PostulationResponseDTO>();
    }

    public async Task<PostulationResponseDTO> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la postulación no puede ser menor o igual a cero.");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"No se encontró ninguna postulación con el ID {id}.");

        return PostulationMapper.ToDto(entity);
    }

    public async Task<PostulationResponseDTO> CreateAsync(PostulationDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "La postulación no puede ser nula.");

 
        await ValidateForeignKeysAsync(dto);

        // 1. Subir archivo a Azure Blob Storage y obtener el blobName
        string blobName = await _blobStorageService.UploadAsync(dto.DocumentFile);

        // 2. Crear la entidad usando el blobName
        var entity = PostulationMapper.ToEntity(dto, blobName);

        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;

        // 3. Guardar en BD
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        // 4. Generar SAS temporal para devolver al frontend
        string sasUrl = _blobStorageService.GenerateSasUrl(entity.DocumentFile);

        // 5. Mapear a DTO de salida
        var response = PostulationMapper.ToDto(entity);
        response.DocumentFileUrl = sasUrl;

        return response;
    }



    public async Task<bool> UpdateAsync(int id, PostulationDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        await ValidateForeignKeysAsync(dto);

        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            throw new KeyNotFoundException($"No existe la postulación con ID {id}.");

        // Actualizar datos base
        existing.UserId = dto.UserId;
        existing.OfferId = dto.OfferId;
        existing.StatusId = dto.StatusId;

        // ¿El usuario subió un nuevo documento?
        if (dto.DocumentFile != null)
        {
            // 1. Subir nuevo archivo
            string newBlobName = await _blobStorageService.UploadAsync(dto.DocumentFile);

            // 2. Reemplazar en base de datos
            existing.DocumentFile = newBlobName;

            // (Opcional) borrar el archivo anterior en Azure
            // await _blobStorageService.DeleteAsync(existing.DocumentFile);
        }

        existing.UpdatedAt = DateTime.Now;

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
