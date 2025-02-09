using Domain.Entity;

namespace Domain.IRepositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryById(Guid categoryId);
    Task<IEnumerable<string>> GetCategoryNames();
    Task<Category?> GetCategoryByCategoryName(string categoryName);
    Task<Category?> AddCategory(Category category);
}