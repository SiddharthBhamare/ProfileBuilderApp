using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileBuilder.AppDbContext;
using ProfileBuilder.Domain.Dto;
using ProfileBuilder.Domain.Model;

namespace ProfileBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ArticleController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetArticles(int userid)
        {
            var user = _dbContext.Users.Where(u => u.UserId == userid).FirstOrDefault();
            if (user == null)
                return NotFound("User not found.");
            try
            {
                var test = _dbContext.Dashboards.FirstOrDefault();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var dashboard = _dbContext.Dashboards?.FirstOrDefault();
            if (dashboard != null && dashboard.DashboardId > 0)
            {
                var articles = _dbContext.Articles.Where(p => p.DashboardId == dashboard.DashboardId).ToList();
                return Ok(articles);
            }
            else
            {
                return NotFound("No photos found.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int articleid)
        {
            var article = _dbContext.Articles.Where(u => u.ArticleId == articleid).FirstOrDefault();
            if (article == null)
                return NotFound("Article not found.");
            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();
            return Ok("Article deleted.");
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArticleRequest articleRequest)
        {
            var article = new Article
            {
                DashboardId = articleRequest.DashboardId,
                Content = articleRequest.Content,
                Title = articleRequest.Title
            };
            var dashboarditem = new UserDashboardItem
            {
                Article = article,
                DashboardId = article.DashboardId
            };
            _dbContext.Articles.Add(article);
            _dbContext.UserDashboardItems.Add(dashboarditem);
            _dbContext.SaveChanges();
            return Ok("New article is created.");
        }
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] ArticleUpdateRequest articleRequest)
        {
            var article = _dbContext.Articles.Where(u => u.ArticleId == articleRequest.ArticleId).FirstOrDefault();
            article.Title = articleRequest.Title;
            article.Content = articleRequest.Content;
            article.UpdatedAt = DateTime.UtcNow;
            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
            return Ok("Article updated.");
        }
    }
}
