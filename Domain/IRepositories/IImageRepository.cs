using Domain.Enity;

namespace Domain.IRepositories;

public interface IImageRepository
{
    Task<Image> GetByCategoryById(Guid categoryId);
    Task<Image?> GetByCategoryByName(string categoryName);
    void DeleteImage(Image image);
    Task UpdateCategoryNameByIdAsync(Guid categoryId, string newName);
    Task AddImageAsync(Image image);
}