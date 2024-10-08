using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("refresh_tokens")]
    [PrimaryKey(nameof(player_id))]
    public class RefreshTokens
    {
        [Required]
        [ForeignKey(nameof(PlayerCredential))]
        public string player_id { get; set; }

        [Required]
        public string refresh_token { get; set; }

        [Required]
        [Column(TypeName = "timestamptz")]
        public DateTime expires_at { get; set; }

        public PlayerCredentials PlayerCredential { get; set; }
    }
}
