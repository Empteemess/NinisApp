using Application.Dto.Category;
using Application.IServices;
using Domain.CustomExceptions;
using Domain.Entity;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Application.Services;

public class CategoryService : ICategoryService

{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
    {
        if(categoryId == Guid.Empty) 
            throw new CategoryException($"category with {categoryId} not found",StatusCodes.Status404NotFound);
        
        var category = await _unitOfWork.CategoryRepository.GetCategoryById(categoryId)
         ?? throw new CategoryException($"category with {categoryId} not found",StatusCodes.Status404NotFound);

        var categoryDto = new CategoryDto
        {
            CategoryName = category.CategoryName,
            ImageUrls = category.Images.Select(x => x.ImageLink)
        };
        return categoryDto;
    }

    public async Task<CategoryDto> AddCategoryAsync(CategoryDto category)
    {
        //IMapper არ გაქ და ხელით გადავმაპე იმედია მუშაობს ორივე :დ 
        var newCategory = new Category
        {
            Id = Guid.NewGuid(),
            CategoryName = category.CategoryName,
            Images = category.ImageUrls?
        .Select(urls => new Image { Id = Guid.NewGuid(), ImageLink = urls})
        .ToList() ?? new List<Image>()
        };

        if (category is null)
            throw new CategoryException($"Invalid category model", StatusCodes.Status400BadRequest);
        var addedCategory = await _unitOfWork.CategoryRepository.AddCategory(newCategory);

        if (addedCategory is null)
            throw new CategoryException($"Couldn't Add Category", StatusCodes.Status500InternalServerError);

        var newCategoryDto = new CategoryDto { CategoryName = addedCategory.CategoryName, 
            ImageUrls = addedCategory.Images.Select(x=> x.ImageLink) };

        return newCategoryDto;
    }

    public async Task<CategoryDto?> GetCategoryByCategoryNameAsync(string categoryName)
    {
        if (categoryName == null)
            throw new CategoryException($"Incorrect category Name format" , StatusCodes.Status400BadRequest);

        var category = await _unitOfWork.CategoryRepository.GetCategoryByCategoryName(categoryName);

        if (category is null)
            throw new CategoryException($"Category Name: {categoryName} Not found", StatusCodes.Status404NotFound);

        var categoryDto  = new CategoryDto { CategoryName = category.CategoryName,
            ImageUrls = category.Images.Select(x=> x.ImageLink) };

        return categoryDto;
    }

    public async Task<IEnumerable<string>> GetCategoryNamesAsync()
    {
        var categoryNames = await _unitOfWork.CategoryRepository.GetCategoryNames();

        //აქ Exception ხო არ ვისროლოთ? NotFound ? 
        if (categoryNames is null)
            return []; 

        return categoryNames;
       
    }
}