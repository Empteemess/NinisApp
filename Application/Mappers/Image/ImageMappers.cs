using Application.Dto;

namespace Application.Mappers.Image;

public static class ImageMappers
{
    
    public static Domain.Enity.Image ToFullUrlImage(this Domain.Enity.Image image,string baseUrl)
    {
        var returnImage = new Domain.Enity.Image
        {
            Id = image.Id,
            ImageLinks = image.ImageLinks.Select(x => $"{baseUrl}/{x}"),
            CategoryName = image.CategoryName,
        };

        return returnImage;
    }
    
    public static Domain.Enity.Image ToImage(this IEnumerable<ImagePath> imagePaths, string categoryName)
    {
        var paths = imagePaths.Select(x => x.Path).ToList();
        
        var image = new Domain.Enity.Image
        {
            ImageLinks = paths,
            CategoryName = categoryName
        };

        return image;
    }
}