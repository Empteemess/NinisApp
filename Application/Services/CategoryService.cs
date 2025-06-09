using Application.Dto.Category;
using Application.IServices;
using Application.Mappers.CategoryMapper;
using Domain.CustomExceptions;
using Domain.Entity;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _baseUrl;

    public CategoryService(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _baseUrl = config["AWS_BASE_URL"]!;
    }

    public async Task AddImageInCategoryAsync(AddImageInCategoryDto addImageInCategoryDto)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(addImageInCategoryDto.CategoryId)
                       ?? throw new CategoryException($"category with {addImageInCategoryDto.CategoryId} not found",
                           StatusCodes.Status404NotFound);

        var images = addImageInCategoryDto.CategoryImages.Select(img => new Image
        {
            ImageLink = img.ImageUrl,
            ImageName = img.ImageName ?? "DefaultImageName"
        });
        
        foreach (var image in images)
        {
            category.Images.Add(image);
        }
        
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await CategoryCheckAsync(categoryId);

        var categoryDto = category.ToCategoryDto(_baseUrl);

        return categoryDto;
    }

    private async Task<Category> CategoryCheckAsync(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new CategoryException($"category with {categoryId} not found", StatusCodes.Status404NotFound);

        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(categoryId)
                       ?? throw new CategoryException($"category with {categoryId} not found",
                           StatusCodes.Status404NotFound);

        return category;
    }

    public async Task AddCategoryAsync(AddCategoryDto addCategory)
    {
        if (addCategory is null)
            throw new CategoryException("Invalid category model", StatusCodes.Status400BadRequest);

        var category = addCategory.ToCategory();

        await _unitOfWork.CategoryRepository.AddCategoryAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CategoryDto?> GetCategoryByCategoryNameAsync(string categoryName)
    {
        if (categoryName is null)
            throw new CategoryException("Incorrect category Name format", StatusCodes.Status400BadRequest);

        var category = await _unitOfWork.CategoryRepository.GetCategoryByCategoryNameAsync(categoryName);
        if (category is null)
            throw new CategoryException($"Category Name: {categoryName} Not found", StatusCodes.Status404NotFound);

        var categoryDto = category.ToCategoryDto(_baseUrl);

        return categoryDto;
    }

    public async Task<IEnumerable<string>> GetCategoryNamesAsync()
    {
        var categoryNames = await _unitOfWork.CategoryRepository.GetCategoryNamesAsync() ?? [];

        return categoryNames;
    }
}