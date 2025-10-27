using System.Collections.Generic;
using TalentoLocal.Models.enums;

namespace TalentoLocal.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        // FK → Postulation
        public int PostulationId { get; set; }

        // Navigation property
        public Postulation Postulation { get; set; } = null!;

        // FK → User (Evaluator)
        public int EvaluatorId { get; set; } // type user (no implement)

        public string Criteria { get; set; } = string.Empty;

        public double Result { get; set; }

        public string Comments { get; set; } = string.Empty;

        public DateTime EvaluationDate { get; set; }

        public EvaluationStatus Status { get; set; } = EvaluationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
