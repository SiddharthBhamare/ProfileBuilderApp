using Microsoft.EntityFrameworkCore;
using ProfileBuilder.Domain.Model;
namespace ProfileBuilder.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<AuthToken>()
                .HasKey(x => x.TokenId);
            modelBuilder.Entity<User>()
               .HasKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Dashboard>()
                .HasKey(x => x.DashboardId);
            modelBuilder.Entity<Article>()
                .HasKey(x => x.ArticleId);
            modelBuilder.Entity<Photo>()
                .HasKey(x => x.PhotoId);
            modelBuilder.Entity<Profile>()
                .HasKey(x => x.ProfileId);
            modelBuilder.Entity<UserDashboardItem>()
                .HasKey(x => x.UserDashboardItemId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
