
using Basket.API.BO.DTOs;

namespace Basket.API.BO.Interfaces;

public interface IBasketService
{
    Task<BasketDTO?> GetBasketByUserId(Guid userId);
    Task<Guid> Insert(Guid userId, Guid productId, string productName, decimal productPrice, int quantity);
}
