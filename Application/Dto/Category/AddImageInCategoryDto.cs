namespace Application.Dto.Category;

public class AddImageInCategoryDto
{
    public Guid CategoryId { get; set; }

    public required IEnumerable<CategoryImageDto> CategoryImages { get; set; }
}