using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class OfferCategoryService : IOfferCategoryService
    {
        private readonly IOfferCategoryRepository _repository;

        public OfferCategoryService(IOfferCategoryRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las categorías
        public async Task<List<OfferCategory>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Obtener categoría por ID
        public async Task<OfferCategory?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("La categoría no existe.");

            return category;
        }

        // Crear nueva categoría
        public async Task<OfferCategory> CreateAsync(OfferCategory offerCategory)
        {
            if (string.IsNullOrWhiteSpace(offerCategory.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            // Validar duplicado
            var existing = await _repository.GetAllAsync();
            bool alreadyExists = existing.Any(c =>
                c.Name != null && c.Name.Trim().ToLower() == offerCategory.Name.Trim().ToLower());

            if (alreadyExists)
                throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

            offerCategory.CreatedAt = DateTime.Now;
            offerCategory.UpdatedAt = DateTime.Now;

            await _repository.AddAsync(offerCategory);
            await _repository.SaveChangesAsync();

            return offerCategory;
        }

        // Actualizar categoría existente
        public async Task<bool> UpdateAsync(int id, OfferCategory offerCategory)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("La categoría no existe.");

            if (string.IsNullOrWhiteSpace(offerCategory.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            // Validar duplicado si cambia el nombre
            if (!string.Equals(existing.Name?.Trim(), offerCategory.Name?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                var allCategories = await _repository.GetAllAsync();
                bool duplicate = allCategories.Any(c =>
                    c.Name != null && c.Name.Trim().ToLower() == offerCategory.Name.Trim().ToLower() && c.Id != id);

                if (duplicate)
                    throw new InvalidOperationException("Ya existe otra categoría con ese nombre.");
            }

            // Actualizar campos
            existing.Name = offerCategory.Name;
            existing.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }

        // Eliminar categoría
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("La categoría no existe.");

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
