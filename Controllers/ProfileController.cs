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
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ProfileController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile(int userid)
        {
            var profile = _dbContext.Profiles.Where(u => u.UserId == userid).FirstOrDefault();
            if (profile == null)
                return NotFound("User not found.");
            return Ok(profile);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = _dbContext.Profiles.ToList();
            if (profiles == null)
                return NotFound("Profiles are empty.");
            return Ok(profiles);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            var profile = _dbContext.Profiles.Where(u => u.ProfileId == profileId).FirstOrDefault();
            if (profile == null)
                return NotFound("Profile not found.");
            _dbContext.Profiles.Remove(profile);
            _dbContext.SaveChanges();
            return Ok("Profile deleted.");
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfileRequest profileRequest)
        {
            var profile = new Profile
            {
                UserId = profileRequest.UserId,
                Bio = profileRequest.Bio,
                ContactInfo = profileRequest.ContactInfo,
                ProfilePictureUrl = profileRequest.ProfilePictureUrl,
                PortfolioUrl = profileRequest.PortfolioUrl
            };
            var dashboard = new Dashboard
            {
                UserId = profileRequest.UserId,
                Title ="Create Title",
            };
            _dbContext.Dashboards.Add(dashboard);//Dashboard should be created once profile is created
            _dbContext.Profiles.Add(profile);
            _dbContext.SaveChanges();
            return Ok("New user profile is created.");
        }
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] UpdateProfileRequest updateProfileRequest)
        {
            var profile = _dbContext.Profiles.Where(u => u.ProfileId == updateProfileRequest.ProfileId).FirstOrDefault();
            profile.PortfolioUrl =updateProfileRequest.PortfolioUrl;
            profile.Bio = updateProfileRequest.Bio;
            profile.ContactInfo = updateProfileRequest.ContactInfo;
            profile.ProfilePictureUrl = updateProfileRequest.ProfilePictureUrl;
            profile.UpdatedAt = DateTime.UtcNow;
           
            _dbContext.Profiles.Add(profile);
            _dbContext.SaveChanges();
            return Ok("Profile updated.");
        }
    }
}
