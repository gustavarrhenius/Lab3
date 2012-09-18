using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }

    public class SportsStoreInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var catWatersport = new Category { CategoryID = 1, Name = "Watersport" };
            var catSoccer = new Category { CategoryID = 2, Name = "Soccer" };
            var catChess = new Category { CategoryID = 3, Name = "Chess" };

            var products = new List<Product>
            {
                new Product { ProductID = 1, Name = @"Kayak", Description = @"A boat for one person", Price = 275M, Category = catWatersport },
                new Product { ProductID = 2, Name = @"Lifejacket", Description = @"Protective and fashionable", Price = 48.95M, Category = catWatersport },
                new Product { ProductID = 3, Name = @"Soccer ball", Description = @"FIFA-approved size and weight", Price = 19.5M, Category = catSoccer },
                new Product { ProductID = 4, Name = @"Corner flags", Description = @"Give your playing field that professional touch", Price = 34.95M, Category = catSoccer },
                new Product { ProductID = 5, Name = @"Stadium", Description = @"Flat-packed 35,000 seat stadium", Price = 79500M, Category = catSoccer },
                new Product { ProductID = 6, Name = @"Thinking cap", Description = @"Improve your brain efficiency by 75%", Price = 16.0M, Category = catChess },
                new Product { ProductID = 7, Name = @"Unsteady chair", Description = @"Secretly give your opponent a disadvantage", Price = 29.95M, Category = catChess },
                new Product { ProductID = 8, Name = @"Human Chess Board", Description = @"A fun game for the whole family", Price = 75.00M, Category = catChess },
                new Product { ProductID = 9, Name = @"Bling-bling King", Description = @"Gold-plated, diamond-studded King", Price = 275M, Category = catChess },

            };
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
        }
    }
}