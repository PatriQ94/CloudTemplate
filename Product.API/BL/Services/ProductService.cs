using MassTransit;
using Product.API.BO.DTOs;
using Product.API.BO.Interfaces;
using Shared.BO.DTOs;

namespace Product.API.BL.Services;

public class ProductService(IProductRepository _productRepository, IPublishEndpoint _publishEndpoint) : IProductService
{
    public async Task<Guid> Insert(string name, string code, decimal price, string? description)
    {
        return await _productRepository.Insert(name, code, price, description);
    }

    public async Task Update(Guid id, string name, decimal price, string? description)
    {
        // Update local data
        await _productRepository.Update(id, name, price, description);

        // Update baskets on Basket API
        await _publishEndpoint.Publish(new ItemUpdatedEvent()
        {
            Id = id,
            Name = name,
            Price = price
        });

        // TODO: update Redis cache
    }

    public async Task<List<ProductDTO>> GetProducts()
    {
        List<BO.Models.Product> products = await _productRepository.GetProducts();
        if (products.Count == 0)
        {
            return [];
        }

        return products.Select(p => new ProductDTO()
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Price = p.Price,
            Description = p.Description
        }).ToList();
    }
}