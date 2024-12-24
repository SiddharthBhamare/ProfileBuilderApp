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
    public class PhotoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public PhotoController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        [HttpGet]   
        public async Task<IActionResult> GetPhotos(int userid)
        {
            var user = _dbContext.Users.Where(u => u.UserId == userid).FirstOrDefault();
            if (user == null)
                return NotFound("User not found.");
            var dashboardid = _dbContext.Dashboards.Where(x => x.UserId == user.UserId).FirstOrDefault()?.DashboardId;
           if(dashboardid > 0)
            {
                var photos = _dbContext.Photos.Where(p => p.DashboardId == dashboardid).ToList();
                return Ok(photos);
            }
            else
            {
                return NotFound("No photos found.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePhoto(int photoid)
        {
            var photo = _dbContext.Photos.Where(u => u.PhotoId == photoid).FirstOrDefault();
            if (photo == null)
                return NotFound("Photo not found.");
            _dbContext.Photos.Remove(photo);
            _dbContext.SaveChanges();
            return Ok("Photo deleted.");
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromBody] UploadRequest upload)
        {
            var photo = new Photo
            {
                DashboardId = upload.DashboardId,
                PhotoUrl = upload.PhotoUrl,
                Description = upload.Description
            };
            _dbContext.Photos.Add(photo);
            _dbContext.SaveChanges();
            return Ok("Photo uploaded.");
        }
    }
}
