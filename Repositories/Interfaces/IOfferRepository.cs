using System.Collections.Generic;
using System.Threading.Tasks;
using TalentoLocal.DTOs;
using TalentoLocal.Models;

namespace TalentoLocal.Repositories.Interfaces
{
    public interface IOfferRepository
    {
        Task<List<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
        Task AddAsync(Offer offer);
        Task UpdateAsync(Offer offer);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();

        Task<List<Offer>> GetOffersByUserIdAsync(string userId);
        Task<List<Offer>> GetByCategoryAsync(string category);

    }
}
