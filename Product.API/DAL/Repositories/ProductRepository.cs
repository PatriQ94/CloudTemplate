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

    // Temporary in-memory list instead of real database
    private static readonly List<Models.Product> _products = [];

    public async Task<Guid> Insert(string name, string code, decimal price, string? description)
    {

        var canConnect = await _context.Database.CanConnectAsync();

        Models.Product? product = _products.Find(p => p.Code == code);
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
        _products.Add(product);
        return product.Id;
    }

    public async Task Update(Guid id, string name, decimal price, string? description)
    {
        var product = _products.Find(p => p.Id == id) ?? throw new Exception($"Product not found");
        product.Name = name;
        product.Price = price;
        product.Description = description;
    }

    public async Task<List<BO.Models.Product>> GetProducts()
    {
        if (_products.Count == 0)
        {
            return [];
        }
        return _products.Select(p => new BO.Models.Product()
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Price = p.Price,
            Description = p.Description
        }).ToList();
    }
}
