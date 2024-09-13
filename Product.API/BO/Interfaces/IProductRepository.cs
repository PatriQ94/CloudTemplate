
namespace Product.API.BO.Interfaces;

public interface IProductRepository
{
    Task<Guid> Insert(string name, string code, decimal price, string? description);
    Task<List<Models.Product>> GetProducts();
}
