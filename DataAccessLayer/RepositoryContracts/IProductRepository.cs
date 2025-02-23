using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace DataAccessLayer.RepositoryContracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();

    Task<IEnumerable<Product?>> GetProductsBy(Expression<Func<Product, bool>> expression);

    Task<Product?> GetProductBy(Expression<Func<Product, bool>> expression);

    Task<Product?> AddProduct(Product product);

    Task<Product?> UpdateProduct(Product product);

    /// <summary>
    ///     deletes a product entity
    /// </summary>
    /// <param name="productId">
    ///     the id of product to be deleted
    /// </param>
    /// <returns>
    ///     returns if deletion is successful or not
    /// </returns>
    Task<bool> DeleteProduct(Guid productId);
}
