using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TalentoLocal.Models.enums;


namespace TalentoLocal.Models
{
    public class Convocation
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ConvocationStatus Type { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;

        public DateTime ClosingDate { get; set; }

        public ConvocationStatus State { get; set; } = ConvocationStatus.Draft;

        public string Location { get; set; } = string.Empty;

        public int AvailablePlaces { get; set; }

        public string Requirements { get; set; } = string.Empty;

        public int PublishingEntityId { get; set; }

        [JsonIgnore]
        public PublishingEntity PublishingEntity { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Relationships
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public ICollection<Postulation> Postulations { get; set; } = new List<Postulation>();
  
    }
}
