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

    //
    // [HttpDelete("{url}")]
    // public async Task<IActionResult> DeletePreSignUrl(string url)
    // {
    //     var deleteRequest = await _storageService.DeletePreSignedUrlAsync(url);
    //     return Ok(new {DeletePreSignUrl = deleteRequest});
    // }

    [HttpPut("signedUrl")]
    public async Task<IActionResult> AddPreSignedUrl(PreSignedUrlRequestDto requestDto)
    {
        var preSignedRequestResult = await _storageService.AddPreSignedUrlAsync(requestDto);
        return Ok(preSignedRequestResult);
    }
}