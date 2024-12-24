using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    [Table("dashboards")]
    public class Dashboard
    {
        [Column("dashboardid")]
        public int DashboardId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("bannerphotourl")]
        public string BannerPhotoUrl { get; set; }

        [Column("businessdetails")]
        public string BusinessDetails { get; set; }

        [Column("shareableurl")]
        public string ShareableUrl { get; set; } 

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation property
        [ForeignKey("userid")]
        public User User { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserDashboardItem> userDashboardItems { get; set; }
    }
}
