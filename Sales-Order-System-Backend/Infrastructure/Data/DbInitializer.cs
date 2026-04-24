using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.Clients.Any())
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Name = "John Doe",
                    Address1 = "No. 1 Main Street",
                    Address2 = "Main Street",
                    Address3 = "Wattala",
                    Suburb = "Colombo",
                    State = "Western",
                    PostCode = 00100
                },
                new Client
                {
                    Name = "Jane Smith",
                    Address1 = "No. 2",
                    Address2 = "Lake Road",
                    Address3 = "Watteama",
                    Suburb = "Kandy",
                    State = "Central",
                    PostCode = 20810
                }
            };

            context.Clients.AddRange(clients);
        }

        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Code = "ITM001",
                    Description = "Laptop",
                    Price = 250000
                },
                new Item
                {
                    Code = "ITM002",
                    Description = "Mouse",
                    Price = 2500
                },
                new Item
                {
                    Code = "ITM003",
                    Description = "Keyboard",
                    Price = 5000
                }
            };

            context.Items.AddRange(items);
        }

        await context.SaveChangesAsync();
    }
}