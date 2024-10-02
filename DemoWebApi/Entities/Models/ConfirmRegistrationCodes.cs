using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("confirm_registration_codes")]
    [PrimaryKey(nameof(email))]
    public class ConfirmRegistrationCodes
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        [Column(TypeName = "timestamptz")]
        public DateTime expire_at { get; set; }
    }
}
