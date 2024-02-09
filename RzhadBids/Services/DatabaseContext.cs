using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Auth;
using RzhadBids.Configuration;
using RzhadBids.Models;
using RzhadBids.Seeder;

namespace RzhadBids.Services
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bid> Bids { get; set; } = null!;
        public DbSet<Lot> Lots { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<LotPhoto> LotPhotos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            Database.EnsureCreated();
            DatabaseSeeder.SeedData(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LotConfig());
        }
    }
}
