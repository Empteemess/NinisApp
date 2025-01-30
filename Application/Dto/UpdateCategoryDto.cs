using System.ComponentModel;

namespace Application.Dto;

public class UpdateCategoryDto
{
    [DefaultValue("categoryId")]
    public required Guid CategoryId { get; set; }
    [DefaultValue("categoryName")]
    public required string NewCategoryName { get; set; }
}