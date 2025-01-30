using Application.Dto;
using Application.IServices;
using Domain.CustomExceptions;
using Domain.Enity;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;

    public ImageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Image> GetComicsByCategoryName(string categoryName)
    {
        var category = await _unitOfWork.IImageRepository.GetByCategoryByName(categoryName);

        if (category is null)
            throw new ImageException($"Category -> {categoryName} not found", StatusCodes.Status404NotFound);

        return category;
    }

    public async Task UpdateCategoryNameAsync(UpdateCategoryDto updateCategoryDto)
    {
        var category = await _unitOfWork.IImageRepository.GetByCategoryById(updateCategoryDto.CategoryId);
        if (category is null)
            throw new ImageException($"Category -> {updateCategoryDto.NewCategoryName} not found",StatusCodes.Status404NotFound);

        await _unitOfWork.IImageRepository.UpdateCategoryNameByIdAsync(updateCategoryDto.CategoryId,
            updateCategoryDto.NewCategoryName);
        await _unitOfWork.SaveChangesAsync();
    }
}