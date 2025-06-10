using Application.IServices;
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

    [HttpDelete]
    public async Task<IActionResult> DeleteImageById([FromQuery] Guid imageId)
    {
        await _imageService.DeleteImageByIdAsync(imageId);
        return NoContent();
    }
    
    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetImageById(Guid imageId)
    {
        var image = await _imageService.GetImageById(imageId);

        return Ok(image);
    }
}