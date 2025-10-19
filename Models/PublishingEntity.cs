using System;
using System.Collections.Generic;
using TalentoLocal.Models.enums;

namespace TalentoLocal.Models

{
    public class PublishingEntity
    {
        public int Id { get; set; }
        public string Name { get; set;} = string.Empty;

        public EntityType Type { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string SiteWeb { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        // relationship with "convocatoria"

    }
}
