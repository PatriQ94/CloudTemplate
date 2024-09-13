using Microsoft.AspNetCore.Mvc;
using Product.API.BO.DTOs;
using Product.API.BO.Interfaces;
using System.Net.Mime;

namespace Product.API.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController, Route("[controller]")]
public class ProductController(IProductService _productService)
{
    [HttpPost("Insert")]
    public async Task<Guid> Insert(ProductDTO product)
    {
        return await _productService.Insert(product.Name, product.Code, product.Price, product.Description);
    }

    [HttpGet("GetProducts")]
    public async Task<List<ProductDTO>> GetProducts()
    {
        return await _productService.GetProducts();
    }
}