using dotNetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNetApp.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { category_id = 1, name = "Fruits", description = "All fruits" },
                new Category { category_id = 2, name = "Electronics", description = "All electronics" },
                new Category { category_id = 3, name = "Clothes", description = "All clothes" }
                );
        }
    }
}
