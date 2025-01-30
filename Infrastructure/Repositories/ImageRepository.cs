using Domain;
using Domain.Enity;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly DbSet<Image> _images;

    public ImageRepository(AppDbContext context)
    {
        _images = context.Set<Image>();
    }

    public async Task AddImageAsync(Image image)
    {
        await _images.AddAsync(image);
    }

    public void DeleteImage(Image image)
    {
        _images.Remove(image);
    }
    
    public async Task UpdateCategoryNameByIdAsync(Guid categoryId, string newName)
    {
        var images = await _images.FirstOrDefaultAsync(x => x.Id == categoryId);
        images.CategoryName = newName;
        
        _images.Update(images);
    }
    public async Task<Image> GetByCategoryById(Guid categoryId)
    {
        var images = await _images
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == categoryId);
        return images;
    }
    public async Task<Image> GetByCategoryByName(string categoryName)
    {
        var images = await _images
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CategoryName == categoryName);
        return images;
    }
}