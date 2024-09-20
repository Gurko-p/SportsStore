using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Contexts;
using SportStore.server.Data.Interfaces;
using SportStore.server.Data.Models;

namespace SportStore.server.Data.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public IQueryable<Category> Query()
    {
        return _context.Categories.AsQueryable();
    }

    public async Task<Category?> FirstOrDefaultAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Category item)
    {
        await _context.Categories.AddAsync(item);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Category item)
    {
        _context.Categories.Update(item);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _context.Categories.FindAsync(id);
        if (order != null)
            _context.Categories.Remove(order);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}