using Domain.Entity;

namespace Domain.IRepositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
    Task<IEnumerable<string>?> GetCategoryNamesAsync();
    Task<Category?> GetCategoryByCategoryNameAsync(string categoryName);
    Task AddCategoryAsync(Category category);
}