using Application.Dto.Storage;
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

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategoryWithImages(Guid categoryId)
    {
        await _storageService.RemoveCategoryByCategoryId(categoryId);
        return NoContent();
    }

    [HttpPut("signedUrl")]
    public async Task<IActionResult> AddPreSignedUrl(PreSignedUrlRequestDto requestDto)
    {
        var preSignedRequestResult = await _storageService.AddPreSignedUrlAsync(requestDto);
        return Ok(preSignedRequestResult);
    }
}