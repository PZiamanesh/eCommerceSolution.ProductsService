using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Product?> AddProduct(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == productId);

        if (product is null)
        {
            return false;
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Product?> GetProductBy(Expression<Func<Product, bool>> expression)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(expression);

        if (product is null)
        {
            return null;
        }

        return product;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsBy(Expression<Func<Product, bool>> expression)
    {
       return await dbContext.Products.Where(expression).ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        var entity = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

        if (entity is null)
        {
            return null;
        }

        entity.ProductName = product.ProductName;
        entity.UnitPrice = product.UnitPrice;
        entity.QuantityInStock = product.QuantityInStock;
        entity.Category = product.Category;

        await dbContext.SaveChangesAsync();

        return product;
    }
}
