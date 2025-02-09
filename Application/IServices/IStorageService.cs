using Application.Dto.Storage;

namespace Application.IServices;

public interface IStorageService
{
    Task<PreSignedUrlResponseDto> GetPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto);
}