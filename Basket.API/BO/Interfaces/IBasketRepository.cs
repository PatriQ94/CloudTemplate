

namespace Basket.API.BO.Interfaces;

public interface IBasketRepository
{
    Task<Models.Basket?> GetBasketByUserId(Guid userId);
    Task<Guid> Insert(Guid userId, Guid productId, string productName, decimal productPrice, int quantity);
}
