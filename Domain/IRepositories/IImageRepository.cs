using Domain.Entity;

namespace Domain.IRepositories;

public interface IImageRepository
{
    Task<Image?> GetImageById(Guid imageId);
}