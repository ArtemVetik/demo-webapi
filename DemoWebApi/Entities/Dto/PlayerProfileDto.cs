using Entities.Enums;

namespace Entities.Dto
{
    public class PlayerProfileDto
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public Gender gender { get; set; }
        public DateTime created_at { get; set; }
        public bool subscribed { get; set; }
        public DateTime? subscription_expires_at { get; set; }
    }
}
