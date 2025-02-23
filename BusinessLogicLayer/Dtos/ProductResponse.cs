namespace BusinessLogicLayer.Dtos;

public record ProductResponse
{
    public Guid ProductID { get; init; }
    public string ProductName { get; init; }
    public CategoryOptions Category { get; init; }
    public double? UnitPrice { get; init; }
    public int? QuantityInStock { get; init; }
}
