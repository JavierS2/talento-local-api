using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentoLocal.Models
{
    [Table("Evaluations")]
    public class Evaluation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("postulation_id")]
        public int PostulationId { get; set; }

        [ForeignKey("PostulationId")]
        public Postulation? Postulation { get; set; }

        [Required]
        [Column("justification")]
        public string Justification { get; set; } = string.Empty;

        [Required]
        [Column("create_at")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("update_at")]
        public DateTime UpdatedAt { get; set; }
    }
}