using Application.Dto;

namespace Application.IServices;

public interface IStorageService
{
    Task AddImageToCategory(AddImageToCategoryDto addImageToCategoryDto);
    Task RemoveCategoryByNameAsync(string categoryName);
    Task UploadFileAsync(GetImageDto getImageDto);
}