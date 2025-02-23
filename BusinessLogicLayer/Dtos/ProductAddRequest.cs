namespace BusinessLogicLayer.Dtos;

public record ProductAddRequest
{
    public string ProductName { get; init; }
    public CategoryOptions Category { get; init; }
    public double? UnitPrice { get; init; }
    public int? QuantityInStock { get; init; }
}
