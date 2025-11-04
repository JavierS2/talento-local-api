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
        public string? Name { get; set; }

        [JsonIgnore] //Delete this after created dto and mappers
        public Postulation? Postulations { get; set; }
        
        [Column("create_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; }

        [Column("update_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }
    }
}