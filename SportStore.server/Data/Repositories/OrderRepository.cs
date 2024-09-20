using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Contexts;
using SportStore.server.Data.Interfaces;
using SportStore.server.Data.Models;

namespace SportStore.server.Data.Repositories;

public class OrderRepository(ApplicationDbContext context) : IRepository<Order>
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public IQueryable<Order> Query()
    {
        return context.Orders.AsQueryable();
    }

    public async Task<Order?> FirstOrDefaultAsync(int id)
    {
        return await context.Orders.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Order item)
    {
        await context.Orders.AddAsync(item);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Order item)
    {
        context.Orders.Update(item);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order != null)
            context.Orders.Remove(order);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}