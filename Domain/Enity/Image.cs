namespace Domain.Enity;

public class Image
{
    public Guid Id { get; set; }
    public required IEnumerable<string>? ImageLinks { get; set; }
    public required string CategoryName { get; set; }
}

