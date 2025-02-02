using Domain.Enity;

namespace Domain.IRepositories;

public interface IImageRepository
{
    void UpdateImageAsync(Image image);
    Task<Image> GetByCategoryById(Guid categoryId);
    Task<Image?> GetByCategoryByName(string categoryName);
    void DeleteImage(Image image);
    Task UpdateCategoryNameByIdAsync(Guid categoryId, string newName);
    Task AddImageAsync(Image image);
}