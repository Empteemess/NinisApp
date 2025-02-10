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
    
    public async Task<Image?> GetImageById(Guid imageId)
    {
      var image = await _context.FirstOrDefaultAsync(x => x.Id == imageId);

        return image;
    }
}