using AutoMapper;
using FluentValidation;
using ProductsMicroService.Core.Dtos;
using ProductsMicroService.Core.Entities;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Core.ServiceContracts;
using System.Linq.Expressions;

namespace ProductsMicroService.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper
        )
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        var productInput = mapper.Map<Product>(productAddRequest);

        var result = await productRepository.AddProduct(productInput);

        return mapper.Map<ProductResponse>(result);
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var result = await productRepository.DeleteProduct(productId);
        return result;
    }

    public async Task<ProductResponse?> GetProductBy(Expression<Func<Product, bool>> expression)
    {
        var product = await productRepository.GetProductBy(expression);

        if (product is null)
        {
            return null;
        }

        return mapper.Map<ProductResponse>(product);
    }

    public async Task<List<ProductResponse>> GetProducts()
    {
        IEnumerable<Product> products = await productRepository.GetProducts();

        return mapper.Map<List<ProductResponse>>(products);
    }

    public async Task<List<ProductResponse>> GetProductsBy(Expression<Func<Product, bool>> expression)
    {
        var products = await productRepository.GetProductsBy(expression);

        return mapper.Map<List<ProductResponse>>(products);
    }

    public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        var product = await productRepository.GetProductBy(p => p.ProductID == productUpdateRequest.ProductID);

        if (product is null)
        {
            throw new ArgumentException("Invalid product Id");
        }

        var updatedProduct = mapper.Map<Product>(productUpdateRequest);

        var updateResult = await productRepository.UpdateProduct(updatedProduct);

        return mapper.Map<ProductResponse>(updateResult);
    }
}
