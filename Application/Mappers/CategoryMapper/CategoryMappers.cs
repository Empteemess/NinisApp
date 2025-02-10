using Application.Dto.Category;
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
    
    
    public static CategoryDto ToCategoryDto(this Category category)
    {
        var categoryDto = new CategoryDto()
        {
            CategoryName = category.CategoryName,
            ImageUrls = category.Images.Select(x => x.ImageLink).ToList()
        };
        
        return categoryDto;
    }
}