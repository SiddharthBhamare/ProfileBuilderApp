using Microsoft.EntityFrameworkCore;
using ProfileBuilder.Domain.Model;
namespace ProfileBuilder.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<UserDashboardItem> UserDashboardItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define table names
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Profile>().ToTable("profiles");
            modelBuilder.Entity<Dashboard>().ToTable("dashboards");
            modelBuilder.Entity<Article>().ToTable("articles");
            modelBuilder.Entity<Photo>().ToTable("photos");
            modelBuilder.Entity<AuthToken>().ToTable("authtokens");
            modelBuilder.Entity<UserDashboardItem>().ToTable("userdashboarditems");

            // User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId).HasColumnName("userid");
                entity.Property(u => u.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
                entity.Property(u => u.PasswordHash).HasColumnName("passwordhash").HasMaxLength(255).IsRequired();
                entity.Property(u => u.FullName).HasColumnName("fullname").HasMaxLength(255).IsRequired();
                entity.Property(u => u.Role).HasColumnName("role").HasMaxLength(50).IsRequired();
                entity.Property(u => u.CreatedAt).HasColumnName("createdat");
                entity.Property(u => u.UpdatedAt).HasColumnName("updatedat");

                entity.HasOne(u => u.Profile)
                      .WithOne(p => p.User)
                      .HasForeignKey<Profile>(p => p.UserId);

                entity.HasMany(u => u.Dashboards)
                      .WithOne(d => d.User)
                      .HasForeignKey(d => d.UserId);
            });

            // Profile entity
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(p => p.ProfileId);
                entity.Property(p => p.ProfileId).HasColumnName("profileid");
                entity.Property(p => p.UserId).HasColumnName("userid");
                entity.Property(p => p.Bio).HasColumnName("bio");
                entity.Property(p => p.ContactInfo).HasColumnName("contactinfo");
                entity.Property(p => p.ProfilePictureUrl).HasColumnName("profilepictureurl").HasMaxLength(500);
                entity.Property(p => p.PortfolioUrl).HasColumnName("portfoliourl").HasMaxLength(500);
                entity.Property(p => p.UpdatedAt).HasColumnName("updatedat");
            });

            // Dashboard entity
            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.HasKey(d => d.DashboardId);
                entity.Property(d => d.DashboardId).HasColumnName("dashboardid");
                entity.Property(d => d.UserId).HasColumnName("userid");
                entity.Property(d => d.Title).HasColumnName("title").HasMaxLength(255);
                entity.Property(d => d.Description).HasColumnName("description");
                entity.Property(d => d.BannerPhotoUrl).HasColumnName("bannerphotourl");
                entity.Property(d => d.BusinessDetails).HasColumnName("businessdetails");
                entity.Property(d => d.ShareableUrl).HasColumnName("shareableurl");
                entity.Property(d => d.CreatedAt).HasColumnName("createdat");
                entity.Property(d => d.UpdatedAt).HasColumnName("updatedat");

                entity.HasMany(d => d.Articles)
                      .WithOne(a => a.Dashboard)
                      .HasForeignKey(a => a.DashboardId);

                entity.HasMany(d => d.Photos)
                      .WithOne(p => p.Dashboard)
                      .HasForeignKey(p => p.DashboardId);
            });

            // Article entity
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(a => a.ArticleId);
                entity.Property(a => a.ArticleId).HasColumnName("articleid");
                entity.Property(a => a.DashboardId).HasColumnName("dashboardid");
                entity.Property(a => a.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
                entity.Property(a => a.Content).HasColumnName("content").IsRequired();
                entity.Property(a => a.CreatedAt).HasColumnName("createdat");
                entity.Property(a => a.UpdatedAt).HasColumnName("updatedat");
            });

            // Photo entity
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(p => p.PhotoId);
                entity.Property(p => p.PhotoId).HasColumnName("photoid");
                entity.Property(p => p.DashboardId).HasColumnName("dashboardid");
                entity.Property(p => p.PhotoUrl).HasColumnName("photourl").HasMaxLength(500).IsRequired();
                entity.Property(p => p.Description).HasColumnName("description");
                entity.Property(p => p.UploadedAt).HasColumnName("uploadedat");
            });

            // AuthToken entity
            modelBuilder.Entity<AuthToken>(entity =>
            {
                entity.HasKey(at => at.TokenId);
                entity.Property(at => at.TokenId).HasColumnName("tokenid");
                entity.Property(at => at.UserId).HasColumnName("userid");
                entity.Property(at => at.Token).HasColumnName("token").HasMaxLength(500).IsRequired();
                entity.Property(at => at.Expiry).HasColumnName("expiry").IsRequired();
                entity.Property(at => at.CreatedAt).HasColumnName("createdat");
            });

            // UserDashboardItem entity
            modelBuilder.Entity<UserDashboardItem>(entity =>
            {
                entity.HasKey(udi => udi.UserDashboardItemId);
                entity.Property(udi => udi.UserDashboardItemId).HasColumnName("userdashboarditemid");
                entity.Property(udi => udi.DashboardId).HasColumnName("dashboardid");
                entity.Property(udi => udi.ArticleId).HasColumnName("articleid");
                entity.Property(udi => udi.PhotoId).HasColumnName("photoid");
                entity.Property(udi => udi.CreatedAt).HasColumnName("createdat");
                entity.Property(udi => udi.UpdatedAt).HasColumnName("updatedat");

                entity.HasOne(udi => udi.Dashboard)
                      .WithMany(d => d.UserDashboardItems)
                      .HasForeignKey(udi => udi.DashboardId);

                entity.HasOne(udi => udi.Article)
                      .WithMany()
                      .HasForeignKey(udi => udi.ArticleId);

                entity.HasOne(udi => udi.Photo)
                      .WithMany()
                      .HasForeignKey(udi => udi.PhotoId);
            });
        }

    }
}
