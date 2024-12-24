
using Microsoft.EntityFrameworkCore;
using ProfileBuilder.AppDbContext;
using ProfileBuilder.Helpers;
using ProfileBuilder.Middlewares;

namespace ProfileBuilderApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Add JWT Authentication using the extension method
            builder.Services.AddJwtAuthentication(
                secretKey: "YourSecretKeyHere",
                issuer: "YourApp",
                audience: "YourApp"
            );
            var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(connectionstring);
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Add Swagger services
            builder.Services.AddSwagger();
            var app = builder.Build();
            app.UseMiddleware<TokenValidationMiddleware>();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            //  app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
