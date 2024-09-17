using Basket.API.BO.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.DAL.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly DBContext _context;

    public BasketRepository(DBContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Guid> Insert(Guid userId, Guid productId, string productName, decimal productPrice, int quantity)
    {
        var userBasket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        // Create a new basket
        if (userBasket == null)
        {
            userBasket = new Models.Basket()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Items = [
                    new Models.Item()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = productId,
                        ProductName = productName,
                        ProductPrice = productPrice,
                        Quantity = quantity
                    }
                ]
            };
            await _context.Baskets.AddAsync(userBasket);
            await _context.SaveChangesAsync();
            return userBasket.Id;
        }

        // Check if the item already exists
        var existingItem = userBasket.Items
            .FirstOrDefault(i => i.ProductId == productId);

        // Add a new item to the existing basket if needed
        if (existingItem == null)
        {
            var newItem = new Models.Item()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ProductName = productName,
                ProductPrice = productPrice,
                Quantity = quantity
            };

            // Add the new item and track it explicitly
            userBasket.Items.Add(newItem);

            // Explicitly mark the item as added in the context
            _context.Entry(newItem).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }
        return userBasket.Id;
    }

    public async Task<BO.Models.Basket?> GetBasketByUserId(Guid userId)
    {
        var userBasket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.UserId == userId);
        if (userBasket == null)
        {
            return null;
        }
        return new BO.Models.Basket()
        {
            Id = userBasket.Id,
            UserId = userBasket.Id,
            Items = userBasket.Items.Select(i => new BO.Models.Item()
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductPrice = i.ProductPrice,
                Quantity = i.Quantity,
            }).ToList()
        };
    }

    public async Task Update(Guid productId, string productName, decimal productPrice)
    {
        await _context.Items
        .Where(b => b.ProductId == productId)
        .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.ProductName, productName)
                .SetProperty(b => b.ProductPrice, productPrice)
        );
    }
}
