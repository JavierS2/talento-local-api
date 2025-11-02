using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentoLocal.Models
{
    [Table("Evaluations", Schema = "public")]
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

        // DDL used 'create_at' and 'uptate_at' (typo). Map to CreatedAt/UpdatedAt
        [Required]
        [Column("create_at", TypeName = "date")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("uptate_at", TypeName = "date")]
        public DateTime UpdatedAt { get; set; }
    }
}