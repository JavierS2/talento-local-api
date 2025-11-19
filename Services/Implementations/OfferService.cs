using Microsoft.EntityFrameworkCore;
using TalentoLocal.DTOs;
using TalentoLocal.Mappers;
using TalentoLocal.Models;
using TalentoLocal.Repositories.Interfaces;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repository;
        private readonly DbDevopsContext _context;

        public OfferService(IOfferRepository repository, DbDevopsContext context)
        {
            _repository = repository;
            _context = context;
        }

        // Obtener todas las ofertas (devuelve lista de DTOs)
        public async Task<List<OfferResponseDTO>> GetAllAsync()
        {
            var offers = await _repository.GetAllAsync();
            Console.WriteLine($"Ofertas recuperadas: {offers.Count}");
            foreach (var o in offers)
                Console.WriteLine($"ID: {o.Id}, Title: {o.Title}");
            return offers.Select(OfferMapper.ToDTO).ToList();
        }


        // Obtener una oferta por ID
        public async Task<OfferResponseDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la oferta debe ser mayor a 0.");

            var offer = await _repository.GetByIdAsync(id);

            if (offer == null)
                throw new KeyNotFoundException($"No se encontró ningún oferta con el ID {id}.");

            return offer != null ? OfferMapper.ToDTO(offer) : null;
        }

        // Crear nueva oferta
        public async Task<OfferResponseDTO> CreateAsync(OfferDTO offerDTO)
        {
            if (offerDTO == null)
                throw new ArgumentNullException(nameof(offerDTO));

            var offer = OfferMapper.ToEntity(offerDTO);

            // 2. Validaciones internas
            ValidateOffer(offer);
            
            await ValidateForeignKeysAsync(offerDTO);

            ValidateOfferStatus(offerDTO.Status); // validamos primero que sea | activo | destacado | urgente

            offer.CreatedAt = DateTime.Now;
            offer.UpdatedAt = DateTime.Now;

            await _repository.AddAsync(offer);
            await _repository.SaveChangesAsync();

            return OfferMapper.ToDTO(offer);
        }

        private async Task ValidateForeignKeysAsync(OfferDTO dto)
        {/*
            // Validar empresa
            bool companyExists = await _context.Companies.AnyAsync(c => c.Id == dto.CompanyId);
            if (!companyExists)
                throw new ArgumentException($"La empresa con ID {dto.CompanyId} no existe.");*/


            // Validar categoría
            bool categoryExists = await _context.OfferCategories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
                throw new ArgumentException($"La categoría con ID {dto.CategoryId} no existe.");
        }



        // Actualizar oferta existente
        public async Task<bool> UpdateAsync(int id, OfferDTO offerDTO)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la oferta debe ser mayor a 0.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró ningún oferta con el ID {id}.");

            var updatedOffer = OfferMapper.ToEntity(offerDTO);
            ValidateOffer(updatedOffer);

            // Actualizamos los campos modificables
            existing.Title = updatedOffer.Title;
            existing.SubTitle = string.IsNullOrWhiteSpace(updatedOffer.SubTitle) ? string.Empty : updatedOffer.SubTitle;
            existing.Description = updatedOffer.Description;
            existing.Modality = updatedOffer.Modality;
            existing.Salary = updatedOffer.Salary;
            existing.Requeriments = updatedOffer.Requeriments;
            existing.Benefits = updatedOffer.Benefits;
            existing.YearsExperience = updatedOffer.YearsExperience;
            existing.Location = updatedOffer.Location;
            existing.Journey = updatedOffer.Journey;
            existing.Schedule = updatedOffer.Schedule;
            existing.AvailablePlaces = updatedOffer.AvailablePlaces;
            ValidateOfferStatus(offerDTO.Status); // validamos primero que sea | activo | destacado | urgente
            existing.Status = updatedOffer.Status;
            existing.ContractType = updatedOffer.ContractType;
            existing.PaymentType = updatedOffer.PaymentType;
            existing.PublicationDate = updatedOffer.PublicationDate;
            existing.ClosingDate = updatedOffer.ClosingDate;
            existing.CompanyId = updatedOffer.CompanyId;
            existing.CategoryId = updatedOffer.CategoryId;
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
                throw new KeyNotFoundException($"No se encontró ningún oferta con el ID {id}.");

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return true;
        }

        // Validaciones de negocio
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
        private static readonly HashSet<string> AllowedStatus = new()
        {
            "activo",
            "destacado",
            "urgente"
        };

        private void ValidateOfferStatus(string status)
        {
            if (!AllowedStatus.Contains(status.ToLower()))
            {
                throw new ArgumentException("El campo 'status' solo puede ser: activo, destacado o urgente.");
            }
        }

    }
}
