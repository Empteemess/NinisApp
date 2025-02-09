using Domain.Entity;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ImageRepository : Domain.IRepositories.IImageRepository
{
    private readonly DbSet<Image> _context;

    public ImageRepository(AppDbContext context)
    {
        _context = context.Set<Image>();
    }
    
    //TODO:GetImageById
    public async Task<Image> GetImageById(Guid imageId)
    {
      var image = await _context.FindAsync(imageId);

        if (image == null)
            throw new ArgumentException($"Image with this : {imageId} id Not Found!");

        return image;
    }
}