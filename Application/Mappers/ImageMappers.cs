using Application.Dto;
using Domain;
using Domain.Enity;

namespace Application.Mappers;

public static class ImageMappers
{
    public static Image ToImage(this IEnumerable<ImagePath> imagePaths, string categoryName)
    {
        var paths = imagePaths.Select(x => x.Path).ToList();
        
        var image = new Image
        {
            ImageLinks = paths,
            CategoryName = categoryName
        };

        return image;
    }
}