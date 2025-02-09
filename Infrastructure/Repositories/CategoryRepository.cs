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

    //TODO:GeAllCategory But Only Names
    public async Task<IEnumerable<string>> GetCategoryNames()
    {
        var categories = await _categories.Select(x => x.CategoryName).ToListAsync();

        return categories;
    }
    //TODO:GetCategoryByCategoryName -> Dto ->(CategoryName -> ImageLinks
    public async Task<Category?> GetCategoryByCategoryName(string name)
    {
        var category = await _categories.FirstOrDefaultAsync(x => x.CategoryName == name);

        return category;
    }
    //TODO:AddCategory (DTO -> CategoryName + Ienumerable<string> imageLinks)
    //აქ ტასკი როგორ გამოვიყენო არ ვიცი და უნდა? await არ ვიყენებ მაინც.
    public async Task<Category?> AddCategory(Category category)
    {
        _categories.Add(category);

        return category;
    }

    public async Task<Category?> GetCategoryById(Guid categoryId)
    {
        var category = await _categories.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == categoryId);
        
        return category;
    }
}