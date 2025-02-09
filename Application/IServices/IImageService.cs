using Application.Dto.Image;

namespace Application.Services;

public interface IImageService
{
    Task<ImageDto> GetImageById(Guid imageId);
}