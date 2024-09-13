
using Product.API.BO.DTOs;

namespace Product.API.BO.Interfaces;

public interface IProductService
{
    Task<List<ProductDTO>> GetProducts();
    Task<Guid> Insert(string name, string code, decimal price, string? description);
    Task Update(Guid id, string name, decimal price, string? description);
}
