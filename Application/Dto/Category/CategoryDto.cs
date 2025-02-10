namespace Application.Dto.Category;

public class CategoryDto
{
    public Guid Id { get; set; }
    public required string CategoryName { get; set; }
    public required IEnumerable<string> ImageUrls { get; set; }

}