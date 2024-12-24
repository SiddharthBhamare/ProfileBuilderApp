using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileBuilder.AppDbContext;
using ProfileBuilder.Domain.Dto;
using ProfileBuilder.Domain.Model;
using ProfileBuilder.Helpers;
namespace ProfileBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public AccountController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Email = request.Email,
                PasswordHash = hashedPassword,
                FullName = request.FullName,
                Role = "User" // Default role
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok("Signup successful.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password.");

            await DeleteTokens(user.UserId);

            // Generate JWT token
            return await GenerateToken(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var authToken = await _dbContext.AuthTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (authToken != null)
            {
                _dbContext.AuthTokens.Remove(authToken);
                await _dbContext.SaveChangesAsync();
            }

            return Ok("Logged out successfully.");
        }
        [HttpPost("deletetokens")]
        public async Task<IActionResult> DeleteTokens(int userid)
        {
            var message = "No tokens found for the user.";
            if (_dbContext.AuthTokens.Any(t => t.UserId == userid))
            {
                _dbContext.AuthTokens.RemoveRange(_dbContext.AuthTokens.Where(x => x.UserId == userid));
                await _dbContext.SaveChangesAsync();
                message = "All tokens deleted successfully.";
            }
            return Ok(message);
        }
        [HttpPost("generatetoken")]
        public async Task<IActionResult> GenerateToken(User user)
        {
            // Generate JWT token
            var token = Helper.GenerateJwtToken(user);

            // Save token in the database
            var authToken = new AuthToken
            {
                UserId = user.UserId,
                Token = token,
                Expiry = DateTime.UtcNow.AddHours(1) // 1-hour expiry
            };

            await _dbContext.AuthTokens.AddAsync(authToken);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Token = token });
        }
    }
}
