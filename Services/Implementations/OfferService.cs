using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repository;

        public OfferService(IOfferRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las ofertas
        public async Task<List<Offer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Obtener oferta por ID
        public async Task<Offer?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la oferta debe ser mayor a 0.");

            var offer = await _repository.GetByIdAsync(id);
            if (offer == null)
                throw new KeyNotFoundException($"No se encontró la oferta con ID {id}.");

            return offer;
        }

        // Crear nueva oferta
        public async Task<Offer> CreateAsync(Offer offer)
        {
            if (offer == null)
                throw new ArgumentNullException(nameof(offer));

            // Validaciones de campos requeridos
            ValidateOffer(offer);

            offer.CreatedAt = DateTime.Now;
            offer.UpdatedAt = DateTime.Now;

            await _repository.AddAsync(offer);
            await _repository.SaveChangesAsync();

            return offer;
        }

        // Actualizar oferta existente
        public async Task<bool> UpdateAsync(int id, Offer offer)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la oferta debe ser mayor a 0.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la oferta con ID {id}.");

            ValidateOffer(offer);

            // Actualizamos los campos modificables
            existing.Title = offer.Title;
            existing.SubTitle = offer.SubTitle;
            existing.Description = offer.Description;
            existing.Modality = offer.Modality;
            existing.Salary = offer.Salary;
            existing.Requeriments = offer.Requeriments;
            existing.Benefits = offer.Benefits;
            existing.YearsExperience = offer.YearsExperience;
            existing.Location = offer.Location;
            existing.Journey = offer.Journey;
            existing.Schedule = offer.Schedule;
            existing.AvailablePlaces = offer.AvailablePlaces;
            existing.Status = offer.Status;
            existing.ContractType = offer.ContractType;
            existing.PaymentType = offer.PaymentType;
            existing.PublicationDate = offer.PublicationDate;
            existing.ClosingDate = offer.ClosingDate;
            existing.CompanyId = offer.CompanyId;
            existing.CategoryId = offer.CategoryId;
            existing.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }

        // Eliminar oferta
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la oferta con ID {id}.");

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return true;
        }

        // Método auxiliar de validación
        private void ValidateOffer(Offer offer)
        {
            if (string.IsNullOrWhiteSpace(offer.Title))
                throw new ArgumentException("El título es obligatorio.");

            if (string.IsNullOrWhiteSpace(offer.Description))
                throw new ArgumentException("La descripción es obligatoria.");

            if (string.IsNullOrWhiteSpace(offer.Modality))
                throw new ArgumentException("La modalidad es obligatoria.");

            if (offer.Salary <= 0)
                throw new ArgumentException("El salario debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(offer.Requeriments))
                throw new ArgumentException("Los requerimientos son obligatorios.");

            if (string.IsNullOrWhiteSpace(offer.Benefits))
                throw new ArgumentException("Los beneficios son obligatorios.");

            if (offer.YearsExperience < 0)
                throw new ArgumentException("Los años de experiencia no pueden ser negativos.");

            if (string.IsNullOrWhiteSpace(offer.Location))
                throw new ArgumentException("La ubicación es obligatoria.");

            if (string.IsNullOrWhiteSpace(offer.Journey))
                throw new ArgumentException("La jornada es obligatoria.");

            if (offer.AvailablePlaces <= 0)
                throw new ArgumentException("Debe haber al menos un cupo disponible.");

            if (string.IsNullOrWhiteSpace(offer.Status))
                throw new ArgumentException("El estado es obligatorio.");

            if (string.IsNullOrWhiteSpace(offer.ContractType))
                throw new ArgumentException("El tipo de contrato es obligatorio.");

            if (string.IsNullOrWhiteSpace(offer.PaymentType))
                throw new ArgumentException("El tipo de pago es obligatorio.");

            if (offer.CompanyId <= 0)
                throw new ArgumentException("Debe especificarse una empresa válida.");

            if (offer.CategoryId <= 0)
                throw new ArgumentException("Debe especificarse una categoría válida.");

            if (offer.PublicationDate == default)
                throw new ArgumentException("Debe establecerse una fecha de publicación.");
        }
    }
}
