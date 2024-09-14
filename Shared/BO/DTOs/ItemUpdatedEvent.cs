﻿namespace Shared.BO.DTOs;

public record ItemUpdatedEvent
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}