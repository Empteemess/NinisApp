using Domain.Entity;

namespace Domain.IRepositories;

public interface IImageRepository
{
    void DeleteImageAsync(Image image);
    Task<IEnumerable<string>> GetImagesByCategoryId(Guid categoryId);
    Task<Image?> GetImageById(Guid imageId);
}