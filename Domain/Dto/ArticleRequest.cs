using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileBuilder.Domain.Dto
{
    public class ArticleRequest
    {
        public int DashboardId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class ArticleUpdateRequest
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
