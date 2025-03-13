using ProductsMicroService.Core.Entities;
using System.Linq.Expressions;

namespace ProductsMicroService.Core.RepositoryContracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();

    Task<IEnumerable<Product>> GetProductsBy(Expression<Func<Product, bool>> expression);

    Task<Product?> GetProductBy(Expression<Func<Product, bool>> expression);

    Task<Product> AddProduct(Product product);

    Task<Product> UpdateProduct(Product product);

    Task<bool> DeleteProduct(Guid productId);
}
