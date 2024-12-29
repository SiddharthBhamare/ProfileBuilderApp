using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileBuilder.AppDbContext;

namespace ProfileBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public DashboardController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;       
        }
        [HttpGet]
        public async Task<IActionResult> GetDashboard(int userid)
        {
            var user = _dbContext.Users.Where(u => u.UserId == userid).FirstOrDefault();
            if (user == null)
                return NotFound("User not found.");
            var dashboard = _dbContext.Dashboards.Where(x => x.UserId == user.UserId).FirstOrDefault();
           
            if (dashboard == null)
                return NotFound("Dashboard not found.");
            var userDashboardItems = _dbContext.UserDashboardItems.Where(x => x.DashboardId == dashboard.DashboardId).ToList();
            dashboard.UserDashboardItems = userDashboardItems;
            return Ok(dashboard);
        }
    }
}
