namespace Basket.API.BO.DTOs;

public class BasketDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<ItemDTO> Items { get; set; } = [];
}
