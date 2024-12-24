using Microsoft.EntityFrameworkCore;
using ProfileBuilder.AppDbContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProfileBuilder.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public TokenValidationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var userId = GetUserIdFromToken(token);

                // Create a scope to resolve ApplicationDbContext
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Check token in the database
                    var tokenRecord = await _context.AuthTokens.FirstOrDefaultAsync(t =>
                        t.Token == token && t.UserId == userId);

                    if (tokenRecord == null || tokenRecord.Expiry < DateTime.UtcNow)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized: Invalid or expired token.");
                        return;
                    }

                    // If the token is valid, set the user in the HttpContext
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                        // Add other claims as needed
                    };

                    var identity = new ClaimsIdentity(claims, "Bearer");
                    var principal = new ClaimsPrincipal(identity);

                    context.User = principal;
                }
            }

            await _next(context);
        }


        private int GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
