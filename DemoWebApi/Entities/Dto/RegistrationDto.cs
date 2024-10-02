using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities.Dto
{
    public class RegistrationDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        [Required]
        [MinLength(6)]
        public string password { get; set; }

        [Required]
        [MinLength(5)]
        public string name { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender gender { get; set; }

        [Required]
        public string confirm_code { get; set; }
    }
}
