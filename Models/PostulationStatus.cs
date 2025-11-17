using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentoLocal.Models
{
    [Table("PostulationsStatus")]
    public class PostulationStatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        public ICollection<Postulation> Postulations { get; set; } = new List<Postulation>();
        
        [Column("create_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; }

        [Column("update_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }
    }
}