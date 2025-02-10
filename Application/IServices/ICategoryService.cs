using Application.Dto.Category;
using Domain.Entity;

namespace Application.IServices;

public interface ICategoryService
{
    Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId);
    Task<CategoryDto?> GetCategoryByCategoryNameAsync(string categoryName);
    Task AddCategoryAsync(AddCategoryDto addCategoryDto);
    Task<IEnumerable<string>> GetCategoryNamesAsync();
}