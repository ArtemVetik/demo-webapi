using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("player_profiles")]
    [PrimaryKey(nameof(player_id))]
    public class PlayerProfiles
    {
        [Required]
        [ForeignKey(nameof(PlayerCredential))]
        public string player_id { get; set; }

        [Required]
        public string name { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender gender {  get; set; }

        [Required]
        [Column(TypeName = "timestamptz")]
        public DateTime created_at { get; set; }

        public virtual PlayerCredentials PlayerCredential { get; set; }
    }
}
