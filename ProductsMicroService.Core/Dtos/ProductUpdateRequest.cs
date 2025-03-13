namespace ProductsMicroService.Core.Dtos;

public record ProductUpdateRequest : ProductAddRequest
{
    public Guid ProductID { get; init; }
}
