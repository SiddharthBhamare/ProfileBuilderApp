using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProfileBuilder.Domain.Model
{
    public class UserDashboardItem
    {
        public int UserDashboardItemId { get; set; }
        public int DashboardId { get; set; }
        public int? ArticleId { get; set; }
        public int? PhotoId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public Dashboard Dashboard { get; set; }
        public Article Article { get; set; }
        public Photo Photo { get; set; }
    }
}
