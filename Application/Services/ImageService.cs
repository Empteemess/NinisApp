using Application.Dto.Image;
using Application.IServices;
using Application.Mappers.ImageMapper;
using Domain.CustomExceptions;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    private readonly IStorageService _storageService;

    public ImageService(IUnitOfWork unitOfWork, IConfiguration config,IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _config = config;
        _storageService = storageService;
    }

    public async Task DeleteImageByIdAsync(Guid imageId)
    {
        var image = await _unitOfWork.ImageRepository.GetImageById(imageId) ??
                    throw new ImageException($"Image {imageId} not found", StatusCodes.Status404NotFound);

        await _storageService.DeleteImageByUrlAsync(image.ImageLink);
        
        _unitOfWork.ImageRepository.DeleteImageAsync(image);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<ImageDto> GetImageById(Guid imageId)
    {
        var image = await _unitOfWork.ImageRepository.GetImageById(imageId) ??
                    throw new ImageException($"Image {imageId} not found", StatusCodes.Status404NotFound);

        var imageDto = image.ToImageDto(_config["AWS_BASE_URL"]!);

        return imageDto;
    }
}