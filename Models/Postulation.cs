using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentoLocal.Models
{
    [Table("Postulations")]
    public class Postulation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("offer_id")]
        public int OfferId { get; set; }

        [ForeignKey("OfferId")]
        public Offer Offer { get; set; } = null!;

        [Column("document_file")]
        public int DocumentFile { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public PostulationStatus? Status { get; set; }
        public Evaluation? Evaluation { get; set; }

        [Column("create_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; set; }

        [Column("update_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; set; }
    }
}