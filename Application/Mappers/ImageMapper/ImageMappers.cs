using Application.Dto.Image;
using Domain.Entity;

namespace Application.Mappers.ImageMapper;

public static class ImageMappers
{
    public static ImageDto ToImageDto(this Image image)
    {
        var imageDto = new ImageDto
        {
            Id = image.Id,
            ImageLink = image.ImageLink,
        };

        return imageDto;
    }
}