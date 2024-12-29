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
            var dashboardid = _dbContext.Dashboards.Where(x => x.UserId == user.UserId).FirstOrDefault()?.DashboardId;
            if (dashboardid > 0)
            {
                var articles = _dbContext.Articles.Where(p => p.DashboardId == dashboardid).ToList();
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
            _dbContext.Articles.Add(article);
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
