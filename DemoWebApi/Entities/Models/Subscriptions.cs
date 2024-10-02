using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("subscriptions")]
    [PrimaryKey(nameof(player_id))]
    public class Subscriptions
    {
        [Required]
        [ForeignKey(nameof(PlayerProfile))]
        public string player_id { get; set; }
        
        [Required]
        [Column(TypeName = "timestamptz")]
        public DateTime expires_at { get; set; }

        public virtual PlayerProfiles PlayerProfile { get; set; }
    }
}
