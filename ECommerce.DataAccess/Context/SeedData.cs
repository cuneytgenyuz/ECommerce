using ECommerce.Entities.Entities;
using ECommerce.Entities.Entities.Concrete;

namespace ECommerce.DataAccess.Context;

public static class SeedData
{
    public static void Seed(this ECommerceDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Electronics", Description = "Electronic devices and gadgets" },
                new Category { Name = "Fashion", Description = "Clothing and accessories" },
                new Category { Name = "Home Appliances", Description = "Appliances for household use" },
                new Category { Name = "Books", Description = "Books and literature" },
                new Category { Name = "Toys", Description = "Toys for kids of all ages" }
            );
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            var electronicsCategory = context.Categories.First(c => c.Name == "Electronics");
            var fashionCategory = context.Categories.First(c => c.Name == "Fashion");
            var homeAppliancesCategory = context.Categories.First(c => c.Name == "Home Appliances");
            var booksCategory = context.Categories.First(c => c.Name == "Books");
            var toysCategory = context.Categories.First(c => c.Name == "Toys");

            context.Products.AddRange(
                // Electronics
                new Product { Name = "Laptop", Price = 1500, Stock = 25, Category = electronicsCategory },
                new Product { Name = "Smartphone", Price = 900, Stock = 40, Category = electronicsCategory },
                new Product { Name = "Headphones", Price = 150, Stock = 100, Category = electronicsCategory },
                new Product { Name = "Smartwatch", Price = 250, Stock = 60, Category = electronicsCategory },

                // Fashion
                new Product { Name = "T-Shirt", Price = 25, Stock = 200, Category = fashionCategory },
                new Product { Name = "Jeans", Price = 50, Stock = 100, Category = fashionCategory },
                new Product { Name = "Sneakers", Price = 75, Stock = 150, Category = fashionCategory },
                new Product { Name = "Jacket", Price = 120, Stock = 50, Category = fashionCategory },

                // Home Appliances
                new Product { Name = "Washing Machine", Price = 500, Stock = 15, Category = homeAppliancesCategory },
                new Product { Name = "Refrigerator", Price = 800, Stock = 10, Category = homeAppliancesCategory },
                new Product { Name = "Microwave Oven", Price = 200, Stock = 30, Category = homeAppliancesCategory },
                new Product { Name = "Vacuum Cleaner", Price = 150, Stock = 20, Category = homeAppliancesCategory },

                // Books
                new Product { Name = "The Great Gatsby", Price = 10, Stock = 100, Category = booksCategory },
                new Product { Name = "To Kill a Mockingbird", Price = 12, Stock = 80, Category = booksCategory },
                new Product { Name = "1984", Price = 15, Stock = 90, Category = booksCategory },
                new Product { Name = "Moby Dick", Price = 18, Stock = 70, Category = booksCategory },

                // Toys
                new Product { Name = "Lego Set", Price = 50, Stock = 60, Category = toysCategory },
                new Product { Name = "Action Figure", Price = 20, Stock = 100, Category = toysCategory },
                new Product { Name = "Board Game", Price = 35, Stock = 40, Category = toysCategory },
                new Product { Name = "Doll", Price = 25, Stock = 80, Category = toysCategory }
            );

            context.SaveChanges();
        }
    }

}