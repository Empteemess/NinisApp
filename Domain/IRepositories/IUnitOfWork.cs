namespace Domain.IRepositories;

public interface IUnitOfWork
{
     Task SaveChangesAsync();
}