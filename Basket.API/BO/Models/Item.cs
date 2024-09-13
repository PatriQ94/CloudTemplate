namespace Basket.API.BO.Models;

public class Item
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required decimal ProductPrice { get; set; }
    public int Quantity { get; internal set; }
}
