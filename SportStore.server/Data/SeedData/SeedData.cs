using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Contexts;
using SportStore.server.Data.Models;

namespace SportStore.server.Data.SeedData;

public static class SeedData
{
    public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        if (!await context.Categories.AnyAsync())
        {
            await context.Categories.AddRangeAsync(
                new Category()
                {
                    CategoryName = "Soccer",
                },
                new Category()
                {
                    CategoryName = "Watersports",
                },
                new Category()
                {
                    CategoryName = "Chess",
                });
        }

        await context.SaveChangesAsync();

        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(
                new Product
                {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    CategoryId = 2,
                    Price = 275
                },
                new Product
                {
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    CategoryId = 2,
                    Price = 48.95m
                },
                new Product
                {
                    Name = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    CategoryId = 1,
                    Price = 19.50m
                },
                new Product
                {
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    CategoryId = 1,
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    CategoryId = 1,
                    Price = 79500
                },
                new Product
                {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    CategoryId = 3,
                    Price = 16
                },
                new Product
                {
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    CategoryId = 3,
                    Price = 29.95m
                },
                new Product
                {
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    CategoryId = 3,
                    Price = 75
                },
                new Product
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated, diamond-studded King",
                    CategoryId = 3,
                    Price = 1200
                }
            );
            await context.SaveChangesAsync();
        }
    }
}