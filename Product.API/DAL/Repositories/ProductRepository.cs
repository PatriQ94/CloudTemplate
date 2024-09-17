using Microsoft.EntityFrameworkCore;
using Product.API.BO.Interfaces;

namespace Product.API.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DBContext _context;

    public ProductRepository(DBContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Guid> Insert(string name, string code, decimal price, string? description)
    {
        Models.Product? product = await _context.Products
            .FirstOrDefaultAsync(p => p.Code == code);
        if (product != null)
        {
            return product.Id;
        }

        product = new Models.Product()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Price = price,
            Description = description
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task Update(Guid id, string name, decimal price, string? description)
    {
        var product = await _context.Products.FindAsync(id) ?? throw new Exception($"Product not found");
        product.Name = name;
        product.Price = price;
        product.Description = description;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BO.Models.Product>> GetProducts()
    {
        return await _context.Products.Select(p => new BO.Models.Product()
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Price = p.Price,
            Description = p.Description
        }).ToListAsync();
    }
}
