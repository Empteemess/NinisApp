namespace Application.Dto.Category;

public class AddCategoryDto
{
    public required string CategoryName { get; set; }
    public required IEnumerable<CategoryImageDto> CategoryImages { get; set; }
}
