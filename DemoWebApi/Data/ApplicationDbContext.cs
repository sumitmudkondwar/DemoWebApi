using DemoWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Shirt> Shirt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Data Seeding
            modelBuilder.Entity<Shirt>().HasData(
                new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men", price = 30, Size = 10 },
                new Shirt { ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men", price = 35, Size = 12 },
                new Shirt { ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "women", price = 28, Size = 8 },
                new Shirt { ShirtId = 4, Brand = "Your Brand", Color = "Yellow", Gender = "women", price = 30, Size = 9 }
            );
        }
    }
}
