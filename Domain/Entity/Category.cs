namespace Domain.Enity;

public class Category
{
    public Guid Id { get; set; }
    public required IEnumerable<string>? ImageLinks { get; set; }
    public required string CategoryName { get; set; }
}

