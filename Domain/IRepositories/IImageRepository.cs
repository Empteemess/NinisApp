using Domain.Entity;

namespace Domain.IRepositories;

public interface IImageRepository
{
    Task<IEnumerable<string>> GetImagesByCategoryId(Guid categoryId);
    Task<Image?> GetImageById(Guid imageId);
}