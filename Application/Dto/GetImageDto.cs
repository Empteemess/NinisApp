using Microsoft.AspNetCore.Http;

namespace Application.Dto;

public class GetImageDto
{
    public required IEnumerable<IFormFile> Images { get; set; }
    public required string CategoryName { get; set; }
}