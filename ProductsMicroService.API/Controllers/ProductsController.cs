using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Core.Dtos;
using ProductsMicroService.Core.ServiceContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsMicroService.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }

    [HttpGet("search/product-id/{productId:guid}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var product = await _productService.GetProductBy(x => x.ProductID == productId);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("search/{searchString}")]
    public async Task<IActionResult> SearchProducts(string searchString)
    {
        var productsByName = await _productService.GetProductsBy(x =>
            x.ProductName != null &&
            x.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));

        var productsByCategory = await _productService.GetProductsBy(x =>
            x.Category != null &&
            x.Category.Contains(searchString, StringComparison.OrdinalIgnoreCase));

        var products = productsByName.Union(productsByCategory);

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest)
    {
        var response = await _productService.AddProduct(productAddRequest);

        if (response is null)
        {
            return Problem("Error in adding product");
        }

        return CreatedAtAction(nameof(GetProductById), new { productId = response.ProductID }, response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        var response = await _productService.UpdateProduct(productUpdateRequest);

        if (response is null)
        {
            return Problem("Error in updating product");
        }

        return Ok(response);
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var result = await _productService.DeleteProduct(productId);

        if (!result)
        {
            return Problem("Error in deleting product");
        }

        return Ok(result);
    }
}