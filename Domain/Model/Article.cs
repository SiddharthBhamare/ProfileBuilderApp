using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProfileBuilder.Domain.Model
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int DashboardId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public Dashboard Dashboard { get; set; }
    }

}
