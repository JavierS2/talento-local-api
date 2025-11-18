using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TalentoLocal.Models
{
    [Table("PostulationsStatus")]
    public class PostulationStatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public List<Postulation> Postulations { get; set; } = new List<Postulation>();

        [Column("create_at")]
        public DateTime CreatedAt { get; set; }

        [Column("update_at")]
        public DateTime UpdatedAt { get; set; }
    }
}