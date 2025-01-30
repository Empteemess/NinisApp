using Application;
using Application.Dto;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpDelete]
    public async Task DeleteComicsByCategoryName(string categoryName)
    {
        await _storageService.RemoveCategoryByNameAsync(categoryName);
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] GetImageDto getImageDto)
    {
        await _storageService.UploadFileAsync(getImageDto);
        return Ok();
    }
}