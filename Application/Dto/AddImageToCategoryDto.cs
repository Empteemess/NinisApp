using Microsoft.AspNetCore.Http;

namespace Application.Dto;

public class AddImageToCategoryDto
{
    public required string CategoryName { get; set; }
    public required IFormFile Image { get; set; }
}