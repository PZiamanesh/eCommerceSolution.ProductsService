using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using FluentValidation;

namespace ProductsMicroService.API.ApiEndpoints;

public static class ProductApiEndpoint
{
    public static IEndpointRouteBuilder MapProductApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (IProductService productService) =>
        {
            var products = await productService.GetProducts();
            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/product-id/{productId:guid}", async (Guid productId, IProductService productService) =>
        {
            var product = await productService.GetProductBy(x => x.ProductID == productId);
            return Results.Ok(product);
        });

        app.MapGet("/api/products/search/{searchString}", async (string searchString, IProductService productService) =>
        {
            var productsByName = await productService.GetProductsBy(x =>
                x.ProductName != null &&
                x.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var productsByCategory = await productService.GetProductsBy(x =>
                x.Category != null &&
                x.Category.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByName.Union(productsByCategory);

            return Results.Ok(products);
        });

        app.MapPost("/api/products", async (
            ProductAddRequest productAddRequest,
            IProductService productService,
            IValidator<ProductAddRequest> productAddRequestValidator
            ) =>
        {
            var validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(k => k.Key, v => v.Select(vld => vld.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var reponse = await productService.AddProduct(productAddRequest);

            if (reponse is null)
            {
                return Results.Problem("Error in adding product");
            }

            return Results.Created($"/api/products/search/product-id/{reponse.ProductID}", reponse);
        });

        app.MapPut("/api/products", async (
            ProductUpdateRequest productUpdateRequest,
            IProductService productService,
            IValidator<ProductUpdateRequest> productUpdateRequestValidator
            ) =>
        {
            var validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(k => k.Key, v => v.Select(vld => vld.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var reponse = await productService.UpdateProduct(productUpdateRequest);

            if (reponse is null)
            {
                return Results.Problem("Error in updating product");
            }

            return Results.Ok(reponse);
        });

        app.MapDelete("/api/products/{productId:guid}", async (Guid productId, IProductService productService) =>
        {
            var result = await productService.DeleteProduct(productId);

            if (!result)
            {
                return Results.Problem("Error in deleting product");
            }

            return Results.Ok(result);
        });

        return app;
    }
}
