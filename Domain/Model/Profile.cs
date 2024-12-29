using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string ContactInfo { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string PortfolioUrl { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }
}
