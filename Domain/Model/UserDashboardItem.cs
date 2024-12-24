using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    [Table("userdashboarditems")]
    public class UserDashboardItem
    {
        [Column("userdashboarditemid")]
        public int UserDashboardItemId { get; set; }
        [Column("dashboardid")]
        public int DashboardId { get; set; }
        [Column("articleid")]
        public int? ArticleId { get; set; }
        [Column("photoid")]
        public int? PhotoId { get; set; }
        [ForeignKey("dashboardid")]
        public Dashboard Dashboard { get; set; }
        [ForeignKey("articleid")]
        public Article? Article { get; set; } = null;
        [ForeignKey("photoid")]
        public Photo? Photo { get; set; } = null;

    }
}
