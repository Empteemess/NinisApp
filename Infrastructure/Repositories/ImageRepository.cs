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
}