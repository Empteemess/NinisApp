using Application.Dto;
using Domain.Enity;

namespace Application.IServices;

public interface IImageService
{
    Task<Image> GetComicsByCategoryName(string categoryName);
    Task UpdateCategoryNameAsync(UpdateCategoryDto updateCategoryDto);
}