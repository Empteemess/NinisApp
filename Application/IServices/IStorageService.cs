using Application.Dto.Storage;

namespace Application.IServices;

public interface IStorageService
{
    Task DeleteImageByUrlAsync(string imageUrl);
    Task RemoveCategoryByCategoryId(Guid categoryId);
    Task<PreSignedUrlResponseDto> AddPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto);
}