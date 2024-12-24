using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    [Table("authtokens")]
    public class AuthToken
    {
        [Column("tokenid")]
        public int TokenId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("expiry")]
        public DateTime Expiry { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
