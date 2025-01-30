using Application.Dto;

namespace Application.IServices;

public interface IStorageService
{
    Task RemoveCategoryByNameAsync(string categoryName);
    Task UploadFileAsync(GetImageDto getImageDto);
}