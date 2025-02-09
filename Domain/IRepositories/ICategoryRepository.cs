using Domain.Entity;

namespace Domain.IRepositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryById(Guid categoryId);
}