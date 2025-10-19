using System;
using TalentoLocal.Models.enums;
namespace TalentoLocal.Models
{
    public class Postulation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        // ID FK
        public int ConvocationId { get; set; }

        // properties navegation
        public Convocation Convocation { get; set; } = null!;

        public DateTime ApplicationDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string AttachedDocument { get; set; } = string.Empty;

        public string CompanyObservation { get; set; } = string.Empty;

        public DateTime? ReviewDate { get; set; }

        public string ActionHistory { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
