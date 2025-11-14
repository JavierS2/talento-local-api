using System.Linq;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

public class OfferCategoryService : IOfferCategoryService
{
    private readonly IOfferCategoryRepository _repository;

    public OfferCategoryService(IOfferCategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<OfferCategoryDTO>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities?.Select(OfferCategoryMapper.ToDto).ToList() ?? new List<OfferCategoryDTO>();
    }

    public async Task<OfferCategoryDTO> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la categoría no puede ser menor o igual a cero.", nameof(id));

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"No se encontró ninguna categoría de oferta con el ID {id}.");

        return OfferCategoryMapper.ToDto(entity);
    }

    public async Task<OfferCategoryDTO> CreateAsync(OfferCategoryDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "La categoría de oferta no puede ser nula.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("El nombre de la categoría es obligatorio.", nameof(dto.Name));

        var offerCategory = OfferCategoryMapper.ToEntity(dto);

        offerCategory.CreatedAt = DateTime.Now;
        offerCategory.UpdatedAt = DateTime.Now;

        await _repository.AddAsync(offerCategory);
        await _repository.SaveChangesAsync();

        // EF Core asigna automáticamente el ID después del SaveChangesAsync
        return OfferCategoryMapper.ToDto(offerCategory);
    }

    public async Task<bool> UpdateAsync(int id, OfferCategoryDTO dto)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la categoría no puede ser menor o igual a cero.", nameof(id));

        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "La categoría de oferta no puede ser nula.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("El nombre de la categoría es obligatorio.", nameof(dto.Name));

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró ninguna categoría de oferta con el ID {id}.");

        existing.Name = dto.Name;
        existing.UpdatedAt = DateTime.Now;

        await _repository.UpdateAsync(existing);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la categoría no puede ser menor o igual a cero.", nameof(id));

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró ninguna categoría de oferta con el ID {id}.");

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return true;
    }
}
