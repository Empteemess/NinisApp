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
        var categories = await _categories.AsNoTracking().Select(x => x.CategoryName).ToListAsync();
        
        return categories;
    }

    public async Task<Category?> GetCategoryByCategoryNameAsync(string name)
    {
        var category = await  _categories.FirstOrDefaultAsync(x => x.CategoryName == name);

        return category;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _categories.AddAsync(category);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await _categories.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == categoryId);

        return category;
    }
}