using Microsoft.AspNetCore.Mvc;
using Product.API.BO.DTOs;
using Product.API.BO.Interfaces;

namespace Product.API.Controllers;

[ApiController, Route("[controller]")]
public class ProductController(IProductService _productService) : ControllerBase
{
    /// <summary>
    /// Adds a new product 
    /// </summary>
    [HttpPost("Insert")]
    public async Task<Guid> Insert(ProductDTO product)
    {
        return await _productService.Insert(product.Name, product.Code, product.Price, product.Description);
    }

    /// <summary>
    /// Adds a new product 
    /// </summary>
    [HttpPost("Update")]
    public async Task Update(ProductDTO product)
    {
        await _productService.Update(product.Id, product.Name, product.Price, product.Description);
    }

    /// <summary>
    /// Returns all existing products
    /// </summary>
    [HttpGet("GetProducts")]
    public async Task<List<ProductDTO>> GetProducts()
    {
        return await _productService.GetProducts();
    }
}