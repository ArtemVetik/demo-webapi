using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
    public class ConfirmEmailDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
