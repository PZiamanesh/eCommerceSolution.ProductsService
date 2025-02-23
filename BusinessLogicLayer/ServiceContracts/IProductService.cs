using BusinessLogicLayer.Dtos;
using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace BusinessLogicLayer.ServiceContracts;

public interface IProductService
{
    Task<List<ProductResponse>> GetProducts();

    Task<List<ProductResponse?>> GetProductsBy(Expression<Func<Product, bool>> expression);

    Task<ProductResponse?> GetProductBy(Expression<Func<Product, bool>> expression);

    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);

    Task<bool> DeleteProduct(Guid productId);
}
