using Basket.API.BO.DTOs;
using Basket.API.BO.Interfaces;

namespace Basket.API.BL.Services;

public class BasketService(IBasketRepository _basketRepository) : IBasketService
{
    public async Task<Guid> Insert(Guid userId, Guid productId, string productName, decimal productPrice, int quantity)
    {
        return await _basketRepository.Insert(userId, productId, productName, productPrice, quantity);
    }

    public async Task Update(Guid productId, string productName, decimal productPrice)
    {
        await _basketRepository.Update(productId, productName, productPrice);
    }

    public async Task<BasketDTO?> GetBasketByUserId(Guid userId)
    {
        BO.Models.Basket? userBasket = await _basketRepository.GetBasketByUserId(userId);
        if (userBasket == null)
        {
            return null;
        }
        return new BasketDTO()
        {
            Id = userBasket.Id,
            UserId = userBasket.UserId,
            Items = userBasket.Items.Select(i => new ItemDTO()
            {
                UserId = userBasket.UserId,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductPrice = i.ProductPrice,
                Quantity = i.Quantity,
            }).ToList(),
        };
    }
}
