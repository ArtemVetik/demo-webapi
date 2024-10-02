using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("custom_maps")]
    [PrimaryKey(nameof(map_id))]
    public class CustomMaps
    {
        [Required]
        public string map_id { get; set; }

        [Required]
        [ForeignKey(nameof(PlayerProfile))]
        public string player_id { get; set; }

        [Required]
        public string download_url { get; set; }

        [Required]
        public string name { get; set; }

        public string? description { get; set; }

        public virtual PlayerProfiles PlayerProfile { get; set; }
    }
}
