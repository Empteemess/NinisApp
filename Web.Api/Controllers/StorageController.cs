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

    [HttpPut("signedUrl")]
    public async Task<IActionResult> GetPreSignedUrl(PreSignedUrlRequestDto requestDto)
    {
        var preSignedRequestResult = await _storageService.GetPreSignedUrlAsync(requestDto);
        return Ok(preSignedRequestResult);
    }
}