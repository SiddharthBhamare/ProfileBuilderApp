using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProfileBuilder.Domain.Model
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int DashboardId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public Dashboard Dashboard { get; set; }
    }
}
