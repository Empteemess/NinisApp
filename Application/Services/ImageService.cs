using Application.Dto;
using Application.IServices;
using Application.Mappers.Image;
using Domain.CustomExceptions;
using Domain.Enity;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    public ImageService(IUnitOfWork unitOfWork,IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    public async Task<Image> GetComicsByCategoryName(string categoryName)
    {
        var category = await _unitOfWork.IImageRepository.GetByCategoryByName(categoryName);

        if (category is null)
            throw new ImageException($"Category -> {categoryName} not found", StatusCodes.Status404NotFound);

        return category.ToFullUrlImage(_config["AWS_BASE_URL"]!);
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