using Microsoft.EntityFrameworkCore;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repository;
        private readonly DbDevopsContext _context;

        public FavoriteService(IFavoriteRepository repository, DbDevopsContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<FavoriteDTO>> GetAllAsync()
        {
            var favorites = await _repository.GetAllAsync();

            return favorites
                .Select(f => FavoriteMapper.ToDTO(f))
                .ToList();
        }

        public async Task<FavoriteDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del favorito debe ser mayor a 0.");

            var favorite = await _repository.GetByIdAsync(id);
            if (favorite == null)
                throw new KeyNotFoundException($"No se encontró el favorito con ID {id}.");

            return FavoriteMapper.ToDTO(favorite);
        }

        public async Task<FavoriteDTO> CreateAsync(FavoriteDTO favoriteDTO)
        {
            if (favoriteDTO == null)
                throw new ArgumentNullException(nameof(favoriteDTO));

            // 🔹 Validación correcta para UserId (string)
            if (string.IsNullOrWhiteSpace(favoriteDTO.UserId))
                throw new ArgumentException("El campo 'UserId' es obligatorio y no puede estar vacío.");

            if (!int.TryParse(favoriteDTO.UserId, out var parsedUserId) || parsedUserId <= 0)
                throw new ArgumentException("El campo 'UserId' debe ser un número válido mayor a cero.");


            // 🔹 Validación para OfferId
            if (favoriteDTO.OfferId <= 0)
                throw new ArgumentException("El campo 'OfferId' es obligatorio y debe ser válido.");

            await ValidateForeignKeysAsync(favoriteDTO, null);

            var entity = FavoriteMapper.ToEntity(favoriteDTO);
            entity.CreatedAt = DateTime.Now;

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return FavoriteMapper.ToDTO(entity);
        }


        public async Task<bool> UpdateAsync(int id, FavoriteDTO favoriteDTO)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del favorito debe ser mayor a 0.");

            if (favoriteDTO == null)
                throw new ArgumentNullException(nameof(favoriteDTO));

            // 🔹 Validación correcta para UserId (string)
            if (string.IsNullOrWhiteSpace(favoriteDTO.UserId))
                throw new ArgumentException("El campo 'UserId' es obligatorio y no puede estar vacío.");

            if (!int.TryParse(favoriteDTO.UserId, out var parsedUserId) || parsedUserId <= 0)
                throw new ArgumentException("El campo 'UserId' debe ser un número válido mayor a cero.");

            if (favoriteDTO.OfferId <= 0)
                throw new ArgumentException("El campo 'OfferId' es obligatorio y debe ser válido.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el favorito con ID {id}.");

            await ValidateForeignKeysAsync(favoriteDTO, id);

            existing.UserId = favoriteDTO.UserId;
            existing.OfferId = favoriteDTO.OfferId;
            // CreatedAt se mantiene

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el favorito con ID {id}.");

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<List<FavoriteDTO>> GetByUserAsync(string userId)
        {
            if (!int.TryParse(userId, out int parsedUserId) || parsedUserId <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return favorites
                .Select(f => FavoriteMapper.ToDTO(f))
                .ToList();
        }

        // Toggle favorito (agregar/quitar)
        public async Task<bool> ToggleAsync(string userId, int offerId)
        {
            if (!int.TryParse(userId, out int parsedUserId) || parsedUserId <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            if (offerId <= 0)
                throw new ArgumentException("El ID de la oferta debe ser mayor a 0.");

            // Validar que la oferta exista
            bool offerExists = await _context.Offers
                .AnyAsync(o => o.Id == offerId);

            if (!offerExists)
                throw new ArgumentException($"La oferta con ID {offerId} no existe.");

            // Buscar si ya existe el favorito
            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.OfferId == offerId);

            if (existing != null)
            {
                _context.Favorites.Remove(existing);
                await _context.SaveChangesAsync();
                return false; // ahora YA NO está en favoritos
            }

            // Si no existe, lo agregamos
            var newFavorite = new Favorite
            {
                UserId = userId,
                OfferId = offerId,
                CreatedAt = DateTime.Now
            };

            await _context.Favorites.AddAsync(newFavorite);
            await _context.SaveChangesAsync();

            return true; // ahora SÍ está en favoritos
        }

        private async Task ValidateForeignKeysAsync(FavoriteDTO dto, int? ignoreId)
        {
            // Validar que la oferta exista
            bool offerExists = await _context.Offers
                .AnyAsync(o => o.Id == dto.OfferId);

            if (!offerExists)
                throw new ArgumentException($"La oferta con ID {dto.OfferId} no existe.");

            // Validar que no exista duplicado (mismo user + offer)
            var query = _context.Favorites
                .Where(f => f.UserId == dto.UserId && f.OfferId == dto.OfferId);

            if (ignoreId.HasValue)
            {
                query = query.Where(f => f.Id != ignoreId.Value);
            }

            bool alreadyFavorite = await query.AnyAsync();

            if (alreadyFavorite)
                throw new ArgumentException(
                    $"El usuario con ID {dto.UserId} ya tiene la oferta {dto.OfferId} en favoritos."
                );
        }
    }
}
