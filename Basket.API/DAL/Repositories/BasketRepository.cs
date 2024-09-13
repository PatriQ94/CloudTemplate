using Basket.API.BO.Interfaces;

namespace Basket.API.DAL.Repositories;

public class BasketRepository : IBasketRepository
{
    // Temporary in-memory list instead of real database
    private static readonly List<Models.Basket> _baskets = [];

    public async Task<Guid> Insert(Guid userId, Guid productId, string productName, decimal productPrice, int quantity)
    {
        var userBasket = _baskets.Find(b => b.UserId == userId);

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
            _baskets.Add(userBasket);
            return userBasket.Id;
        }

        // Add a new item to the existing basket if needed
        if (!userBasket.Items.Exists(i => i.ProductId == productId))
        {
            userBasket.Items.Add(new Models.Item()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ProductName = productName,
                ProductPrice = productPrice,
                Quantity = quantity
            });
        }
        return userBasket.Id;
    }

    public async Task<BO.Models.Basket?> GetBasketByUserId(Guid userId)
    {
        var userBasket = _baskets.Find(b => b.UserId == userId);
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
}
