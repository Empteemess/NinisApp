using Application.Dto.Storage;

namespace Application.IServices;

public interface IStorageService
{
    // Task<string> DeletePreSignedUrlAsync(string url);    
    Task<PreSignedUrlResponseDto> AddPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto);
}