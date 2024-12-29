using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{

    [Table("users")]
    public class User
    {
        [Column("userid")]
        public int UserId { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("passwordhash")]
        public string PasswordHash { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("role")]
        public string Role { get; set; } // "User" or "Admin"

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }


}
