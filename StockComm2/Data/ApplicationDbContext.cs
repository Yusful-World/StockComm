using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockComm.Models;

namespace StockComm.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserPortfolio> UserPortfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<UserPortfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

                modelBuilder.Entity<UserPortfolio>()
                    .HasOne(u => u.AppUser)
                    .WithMany(u => u.Portfolios)
                    .HasForeignKey(u => u.AppUserId);

                modelBuilder.Entity<UserPortfolio>()
                    .HasOne(u => u.Stock)
                    .WithMany(u => u.Portfolios)
                    .HasForeignKey(u => u.StockId);

                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                };
                
                modelBuilder.Entity<IdentityRole>().HasData(roles);
            }
        }
    }
   
}
