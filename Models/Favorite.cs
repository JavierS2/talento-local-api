using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentoLocal.Models
{
    [Table("Favorites")]
    public class Favorite
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

        // FK hacia Offers
        [ForeignKey("OfferId")]
        public Offer? Offer { get; set; }

        [Required]
        [Column("create_at")]
        public DateTime CreatedAt { get; set; }
    }
}
