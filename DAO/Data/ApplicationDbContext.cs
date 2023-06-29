
using dotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotNet.DAO.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> //dbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        //ENTITIES MAPPER
        public DbSet<Category> Categories {get; set;}
        public DbSet<Product> Products { get; set;} 
        public DbSet<ApplicationUser> ApplicationUsers { get; set;}
        public DbSet<Order> Orders { get; set;}
        //SEEDERS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
                modelBuilder.Entity<Category>().HasData(
                new Category { category_id = 1, name = "Fruits", DisplayOrder = 1 },
                new Category { category_id = 2, name = "Electronics", DisplayOrder = 2 },
                new Category { category_id = 3, name = "Clothes", DisplayOrder = 3 }
                );
            
           

                modelBuilder.Entity<Product>().HasData(
                  new Product
                  {
                      Id = 1,
                      Title = "Fortune of Time",
                      Author = "Billy Spark",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "SWD9999001",
                      ListPrice = 99,
                      Price = 90,
                      Price50 = 85,
                      Price100 = 80,
                      CategoryId = 1,
                      ImgUrl = ""

                  },
                  new Product
                  {
                      Id = 2,
                      Title = "Dark Skies",
                      Author = "Nancy Hoover",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "CAW777777701",
                      ListPrice = 40,
                      Price = 30,
                      Price50 = 25,
                      Price100 = 20,
                      CategoryId = 1,
                      ImgUrl = ""

                  },
                  new Product
                  {
                      Id = 3,
                      Title = "Vanish in the Sunset",
                      Author = "Julian Button",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "RITO5555501",
                      ListPrice = 55,
                      Price = 50,
                      Price50 = 40,
                      Price100 = 35,
                      CategoryId = 2,
                      ImgUrl = ""

                  },
                  new Product
                  {
                      Id = 4,
                      Title = "Cotton Candy",
                      Author = "Abby Muscles",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "WS3333333301",
                      ListPrice = 70,
                      Price = 65,
                      Price50 = 60,
                      Price100 = 55,
                      CategoryId = 2,
                      ImgUrl = ""

                  },
                  new Product
                  {
                      Id = 5,
                      Title = "Rock in the Ocean",
                      Author = "Ron Parker",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "SOTJ1111111101",
                      ListPrice = 30,
                      Price = 27,
                      Price50 = 25,
                      Price100 = 20,
                      CategoryId = 3,
                      ImgUrl = ""

                  },
                  new Product
                  {
                      Id = 6,
                      Title = "Leaves and Wonders",
                      Author = "Laura Phantom",
                      Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                      ISBN = "FOT000000001",
                      ListPrice = 25,
                      Price = 23,
                      Price50 = 22,
                      Price100 = 20,
                      CategoryId = 3,
                      ImgUrl = ""

                  }
                  );
            
        }
        

        
   
           
        
    }
}
