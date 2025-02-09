namespace Domain.IRepositories;

public interface IUnitOfWork
{
     IImageRepository ImageRepository { get; }
     ICategoryRepository CategoryRepository { get; }
     Task SaveChangesAsync();
}