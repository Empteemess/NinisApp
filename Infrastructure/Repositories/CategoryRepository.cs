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
    //TODO:GetCategoryByCategoryName -> Dto ->(CategoryName -> ImageLinks) 
    //TODO:AddCategory (DTO -> CategoryName + Ienumerable<string> imageLinks)
    
    public async Task<Category?> GetCategoryById(Guid categoryId)
    {
        var category = await _categories.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == categoryId);
        
        return category;
    }
}