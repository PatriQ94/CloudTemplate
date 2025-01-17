﻿namespace Product.API.BO.Models;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
