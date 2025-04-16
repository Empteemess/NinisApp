using Domain.Entity;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly DbSet<Image> _context;

    public ImageRepository(AppDbContext context)
    {
        _context = context.Set<Image>();
    }

    public void DeleteImageAsync(Image image)
    {
        _context.Remove(image);
    }
    
    public async Task<Image?> GetImageById(Guid imageId)
    {
      var image = await _context.FirstOrDefaultAsync(x => x.Id == imageId);

        return image;
    }
    public async Task<IEnumerable<string>> GetImagesByCategoryId(Guid categoryId)
    {
        var image = _context.AsNoTracking().Where(x => x.CategoryId == categoryId);

        var imageUrls = image.Select(x => x.ImageLink);
        
        return imageUrls;
    }
}