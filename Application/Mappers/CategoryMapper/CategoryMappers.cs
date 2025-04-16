using Application.Dto.Category;
using Application.Dto.Image;
using Domain.Entity;

namespace Application.Mappers.CategoryMapper;

public static class CategoryMappers
{
    public static AddCategoryDto ToAddCategoryDto(this Category category)
    {
        var categoryDto = new AddCategoryDto
        {
            CategoryName = category.CategoryName,
            ImageUrls = category.Images.Select(x => x.ImageLink)
        };

        return categoryDto;
    }

    public static Category ToCategory(this AddCategoryDto category)
    {
        var categoryDto = new Category
        {
            CategoryName = category.CategoryName,
            Images = category.ImageUrls.Select(url => new Image { ImageLink = url }).ToList()
        };

        return categoryDto;
    }

    public static CategoryDto ToCategoryDto(this Category category,string baseUrl)
    {
        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            ImageUrls = category.Images.Select(image => new ImageDto
            {
                Id = image.Id,
                ImageLink = $"{baseUrl}/{image.ImageLink}"
            })
        };

        return categoryDto;
    }
}