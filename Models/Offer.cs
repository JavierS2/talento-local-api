using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentoLocal.Models
{
    [Table("Offers", Schema = "public")]
    public class Offer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("subtitle")]
        public string? SubTitle { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("modality")]
        public string Modality { get; set; } = string.Empty;

        [Required]
        [Column("salary")]
        public int Salary { get; set; }

        [Required]
        [Column("requeriments")]
        public string Requeriments { get; set; } = string.Empty;

        [Required]
        [Column("benefits")]
        public string Benefits { get; set; } = string.Empty;

        [Required]
        [Column("years_experience")]
        public int YearsExperience { get; set; }

        [Required]
        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Column("journey")]
        public string Journey { get; set; } = string.Empty;

        [Column("schedule")]
        public string? Schedule { get; set; }

        [Required]
        [Column("available_places")]
        public int AvailablePlaces { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Column("contract_type")]
        public string ContractType { get; set; } = string.Empty;

        [Required]
        [Column("payment_type")]
        public string PaymentType { get; set; } = string.Empty;

        [Required]
        [Column("publication_date", TypeName = "date")]
        public DateTime PublicationDate { get; set; }

        [Column("closing_date", TypeName = "date")]
        public DateTime? ClosingDate { get; set; }

        [Required]
        [Column("company_id")]
        public int CompanyId { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public OfferCategory? Category { get; set; }
        
        public ICollection<Postulation>? Postulations { get; set; }

        [Column("create_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; }

        [Column("update_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }
    }
}