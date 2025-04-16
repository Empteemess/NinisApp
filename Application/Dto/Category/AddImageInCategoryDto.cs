namespace Application.Dto.Category;

public class AddImageInCategoryDto
{
    public Guid CategoryId { get; set; }

    public required IEnumerable<string> ImageUrls { get; set; }
}