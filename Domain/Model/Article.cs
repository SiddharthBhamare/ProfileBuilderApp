using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    [Table("articles")]
    public class Article
    {
        [Column("articleid")]
        public int ArticleId { get; set; }
        [Column("dashboardid")]
        public int DashboardId { get; set; }
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation property
        [ForeignKey("dashboardid")]
        public Dashboard Dashboard { get; set; }
    }
}
