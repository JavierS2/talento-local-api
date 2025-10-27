using System;
using System.Collections.Generic;
using TalentoLocal.Models.enums;
using TalentoLocal.Models;
using System.Text.Json.Serialization;

namespace TalentoLocal.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public int IdConvocation { get; set; }

        [JsonIgnore]
        public Convocation Convocation { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Mode Mode { get; set; }

        public string Duration { get; set; } = string.Empty;

        public string SpecificRequirements { get; set; } = string.Empty;

        public int MaximumQuota { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
