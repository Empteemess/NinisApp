namespace Application.Dto.Category;

public class CategoryDto
{
    public required string CategoryName { get; set; }
    public IEnumerable<string>? ImageUrls { get; set; }
}
