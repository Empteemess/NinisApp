using Domain.IRepositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context,
        ICategoryRepository categoryRepository,
        IImageRepository imageRepository)
    {
        _context = context;
        CategoryRepository = categoryRepository;
        ImageRepository = imageRepository;
    }

    public IImageRepository ImageRepository { get; }
    public ICategoryRepository CategoryRepository { get; }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}