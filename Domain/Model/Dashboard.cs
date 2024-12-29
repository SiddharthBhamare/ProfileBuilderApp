using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProfileBuilder.Domain.Model
{
    public class Dashboard
    {
        public int DashboardId { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? BannerPhotoUrl { get; set; }
        public string? BusinessDetails { get; set; }
        public string? ShareableUrl { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public User User { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserDashboardItem> UserDashboardItems { get; set; }
    }
}
