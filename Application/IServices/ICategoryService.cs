using Domain.Entity;

namespace Application.IServices;

public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
}