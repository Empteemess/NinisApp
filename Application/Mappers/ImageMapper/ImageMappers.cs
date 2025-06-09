using Application.Dto.Image;
using Domain.Entity;

namespace Application.Mappers.ImageMapper;

public static class ImageMappers
{
    public static ImageDto ToImageDto(this Image image,string baseUrl)
    {
        var imageDto = new ImageDto
        {
            Id = image.Id,
            ImageUrl = $"{baseUrl}/{image.ImageLink}",
            ImageName = image.ImageName ?? "DefaultImageName"
        };

        return imageDto;
    }
}