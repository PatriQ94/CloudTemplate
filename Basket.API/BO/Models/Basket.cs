namespace Basket.API.BO.Models;

public class Basket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<Item> Items { get; set; } = [];
}
