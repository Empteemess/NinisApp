using Application.Dto.Category;
using Application.Dto.Image;
using Domain.Entity;

namespace Application.Mappers.CategoryMapper;

public static class CategoryMappers
{
    public static Category ToCategory(this AddCategoryDto category)
    {
        var categoryDto = new Category
        {
            CategoryName = category.CategoryName,
            Images = category.CategoryImages.Select(url => new Image
            {
                ImageLink = url.ImageUrl,
                ImageName = url.ImageName
            }).ToList()
        };

        return categoryDto;
    }

    public static CategoryDto ToCategoryDto(this Category category,string baseUrl)
    {
        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Images = category.Images.Select(image => new ImageDto
            {
                Id = image.Id,
                ImageUrl = $"{baseUrl}/{image.ImageLink}",
                ImageName = image.ImageName ?? "DefaultImageName"
            })
        };

        return categoryDto;
    }
}