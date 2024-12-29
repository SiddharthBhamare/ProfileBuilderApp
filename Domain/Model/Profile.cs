using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    [Table("profiles")]
    public class Profile
    {
        [Column("profileid")]
        public int ProfileId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("bio")]
        public string Bio { get; set; }

        [Column("contactinfo")]
        public string ContactInfo { get; set; }

        [Column("profilepictureurl")]
        public string ProfilePictureUrl { get; set; }

        [Column("portfoliourl")]
        public string PortfolioUrl { get; set; } 

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("userid")]
        public User User { get; set; }
    }
}
