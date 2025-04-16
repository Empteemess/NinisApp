using Application.Dto.Image;

namespace Application.IServices;

public interface IImageService
{
    Task DeleteImageByIdAsync(Guid imageId);
    Task<ImageDto> GetImageById(Guid imageId);
}