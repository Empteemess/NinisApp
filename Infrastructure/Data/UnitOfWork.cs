using Domain.IRepositories;
using Infrastructure.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context, IImageRepository imageRepository)
    {
        _context = context;
        IImageRepository = imageRepository;
    }

    public IImageRepository IImageRepository { get; }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}