namespace Domain.Entity;

public class Image
{
    public Guid Id { get; set; }
    public required string ImageLink { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}