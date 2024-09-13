namespace Basket.API.BO.DTOs;

public record ItemDTO
{
    public Guid UserId { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required decimal ProductPrice { get; set; }
    public required int Quantity { get; set; }
}
