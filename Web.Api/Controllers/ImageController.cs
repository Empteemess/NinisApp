using Application.Dto;
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

    [HttpPut]
    public async Task<IActionResult> UpdateComicsName(UpdateCategoryDto updateCategoryDto)
    {
        await _imageService.UpdateCategoryNameAsync(updateCategoryDto);
        return Ok($"updated to {updateCategoryDto.NewCategoryName}");
    }

    [HttpGet("{categoryName}")]
    public async Task<IActionResult> GetComicsByCategoryName(string categoryName)
    {
        var comics = await _imageService.GetComicsByCategoryName(categoryName);
        return Ok(comics);
    }
}