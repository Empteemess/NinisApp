using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetImageById(Guid imageId)
    {
        var image = await _imageService.GetImageById(imageId);

        return Ok(image);
    }
}