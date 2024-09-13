using Product.API.BO.DTOs;
using Product.API.BO.Interfaces;

namespace Product.API.BL.Services;

public class ProductService(IProductRepository _productRepository) : IProductService
{
    public async Task<Guid> Insert(string name, string code, decimal price, string? description)
    {
        return await _productRepository.Insert(name, code, price, description);
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
