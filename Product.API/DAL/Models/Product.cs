namespace Product.API.DAL.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
