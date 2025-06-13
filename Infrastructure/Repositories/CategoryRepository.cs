using System.Net;
using Domain.CustomExceptions;
using Domain.Entity;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _categories;

    public CategoryRepository(AppDbContext context)
    {
        _categories = context.Set<Category>();
    }

    public async Task<IEnumerable<string>?> GetCategoryNamesAsync()
    {
        var characterCategories = new List<string> { "body", "bottom", "hair", "shoes", "top","backgrounds" };

        var categories = await _categories
            .AsNoTracking()
            .Select(x => x.CategoryName)
            .ToListAsync();


        var filteredCategoryNames =
            categories.Where(x => !characterCategories.Contains(x, StringComparer.OrdinalIgnoreCase));

        return filteredCategoryNames;
    }

    public async Task<Category?> GetCategoryByCategoryNameAsync(string name)
    {
        var category = await _categories.AsNoTracking()
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.CategoryName.ToLower() == name.ToLower());

        return category;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _categories.AddAsync(category);
    }

    public async Task DeleteCategoryById(Guid categoryId)
    {
        var category = await _categories.FirstOrDefaultAsync(x => x.Id == categoryId);

        var ss = _categories.Remove(category);

        if (ss.State != EntityState.Deleted)
            throw new CategoryException($"Category {categoryId} cannot be deleted", (int)HttpStatusCode.BadRequest);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await _categories.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == categoryId);

        return category;
    }
}