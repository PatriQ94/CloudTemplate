using Basket.API.BO.DTOs;
using Basket.API.BO.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController, Route("[controller]")]
public class BasketController(IBasketService _basketService) : ControllerBase
{
    /// <summary>
    /// Adds a new item to the users basket
    /// </summary>
    [HttpPost("Insert")]
    public async Task<Guid> Insert(ItemDTO item)
    {
        return await _basketService.Insert(item.UserId, item.ProductId, item.ProductName, item.ProductPrice, item.Quantity);
    }

    /// <summary>
    /// Returns all items in a basket for a specific user
    /// </summary>
    [HttpGet("GetBasketByUserId")]
    public async Task<BasketDTO?> GetBasketByUserId([FromQuery] Guid UserId)
    {
        return await _basketService.GetBasketByUserId(UserId);
    }

    /// <summary>
    /// Adds a new item to the users basket
    /// </summary>
    [HttpPost("UpdateItem")]
    public async Task<bool> UpdateItem(ItemDTO item)
    {
        return true;
        //return await _basketService.Insert(item.UserId, item.ProductId, item.ProductName, item.ProductPrice, item.Quantity);
    }
}
