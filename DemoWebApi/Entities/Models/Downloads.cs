using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("downloads")]
    [PrimaryKey(nameof(player_id), nameof(map_id))]
    public class Downloads
    {
        [Required]
        [ForeignKey(nameof(PlayerProfile))]
        public string player_id { get; set; }
        
        [Required]
        [ForeignKey(nameof(CustomMap))]
        public string map_id { get; set; }

        public virtual PlayerProfiles PlayerProfile { get; set; }

        public virtual CustomMaps CustomMap { get; set; }
    }
}
