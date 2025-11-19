using Microsoft.EntityFrameworkCore;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;

namespace TalentoLocal.Repositories.Implementations
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DbDevopsContext _context;

        public FavoriteRepository(DbDevopsContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await _context.Favorites
                .Include(f => f.Offer)
                .ToListAsync();
        }

        public async Task<Favorite?> GetByIdAsync(int id)
        {
            return await _context.Favorites
                .Include(f => f.Offer)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
        }

        public Task UpdateAsync(Favorite favorite)
        {
            _context.Favorites.Update(favorite);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var favorite = await GetByIdAsync(id);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
