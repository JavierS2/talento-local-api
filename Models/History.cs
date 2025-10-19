using System;
using TalentoLocal.Models.enums;
namespace TalentoLocal.Models
{
    public class History
    {
        public int Id { get; set; }

        public EventType EventType { get; set; }

        public int ResponsibleUserId { get; set; }

        public int ReferenceId { get; set; }

        public string EventDescription { get; set; } = string.Empty;

        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        public string PreviousState { get; set; } = string.Empty;

        public string NewState { get; set; } = string.Empty;

        public string Observations { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
