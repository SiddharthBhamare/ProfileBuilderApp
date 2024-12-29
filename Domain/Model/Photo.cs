using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Model
{
    [Table("photos")]
    public class Photo
    {
        [Column("photoid")]
        public int PhotoId { get; set; }
        [Column("dashboardid")]
        public int DashboardId { get; set; }

        [Column("photourl")]
        public string PhotoUrl { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("uploadedat")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("dashboardid")]
        public Dashboard Dashboard { get; set; }
    }
}
