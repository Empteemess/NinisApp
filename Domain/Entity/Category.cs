namespace Domain.Entity;

public class Category
{
    public Guid Id { get; set; }
    public required string CategoryName { get; set; }
    public required ICollection<Image> Images { get; set; }
}

