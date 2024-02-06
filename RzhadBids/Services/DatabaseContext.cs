using Microsoft.EntityFrameworkCore;
using RzhadBids.Configuration;
using RzhadBids.Models;

namespace RzhadBids.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Bid> Bids { get; set; } = null!;
        public DbSet<Lot> Lots { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<LotPhoto> LotPhotos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LotConfig());
        }
    }
}
