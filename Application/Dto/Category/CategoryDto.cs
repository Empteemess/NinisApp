using Application.Dto.Image;

namespace Application.Dto.Category;

public class CategoryDto
{
    public Guid Id { get; set; }
    public required string CategoryName { get; set; }
    public required IEnumerable<ImageDto> Images { get; set; }

}