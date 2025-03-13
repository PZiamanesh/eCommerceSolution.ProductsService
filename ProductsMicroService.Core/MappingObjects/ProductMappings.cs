using AutoMapper;
using ProductsMicroService.Core.Dtos;
using ProductsMicroService.Core.Entities;

namespace ProductsMicroService.Core.MappingObjects;

internal class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<ProductAddRequest, Product>()
            .ForMember(d => d.ProductName, x => x.MapFrom(s => s.ProductName))
            .ForMember(d => d.UnitPrice, x => x.MapFrom(s => s.UnitPrice))
            .ForMember(d => d.QuantityInStock, x => x.MapFrom(s => s.QuantityInStock))
            .ForMember(d => d.Category, x => x.MapFrom(s => s.Category));

        CreateMap<ProductUpdateRequest, Product>()
            .ForMember(d => d.ProductID, x => x.MapFrom(s => s.ProductID))
            .ForMember(d => d.ProductName, x => x.MapFrom(s => s.ProductName))
            .ForMember(d => d.UnitPrice, x => x.MapFrom(s => s.UnitPrice))
            .ForMember(d => d.QuantityInStock, x => x.MapFrom(s => s.QuantityInStock))
            .ForMember(d => d.Category, x => x.MapFrom(s => s.Category));

        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.ProductID, x => x.MapFrom(s => s.ProductID))
            .ForMember(d => d.ProductName, x => x.MapFrom(s => s.ProductName))
            .ForMember(d => d.UnitPrice, x => x.MapFrom(s => s.UnitPrice))
            .ForMember(d => d.QuantityInStock, x => x.MapFrom(s => s.QuantityInStock))
            .ForMember(d => d.Category, x => x.MapFrom(s => s.Category));
    }
}
