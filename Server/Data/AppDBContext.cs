using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDBContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder);
            Category[] catergoriesToSeed = new Category[3];

            for (int i = 0; i < catergoriesToSeed.Length; i++)
            {
                Category category = new Category { 
                    CategoryId = i, 
                    Description = "DEs" + i.ToString(), 
                    Name = $"Catergory[i]", 
                    ThumbnailImagePath = "uploads/placeholder.jpg" };

                catergoriesToSeed[i] = category;
            }

            modelBuilder.Entity<Category>().HasData(catergoriesToSeed);
        }
    }
}
