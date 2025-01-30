namespace Domain.IRepositories;

public interface IUnitOfWork
{
     IImageRepository IImageRepository { get;}
     Task SaveChangesAsync();
}