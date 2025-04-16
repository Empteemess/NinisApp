using Application.Dto.Storage;

namespace Application.IServices;

public interface IStorageService
{
    Task RemoveCategoryByCategoryId(Guid categoryId);
    Task<PreSignedUrlResponseDto> AddPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto);
}