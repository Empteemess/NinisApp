using Application.Dto.Image;
using Application.Mappers.ImageMapper;
using Domain.CustomExceptions;
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

    public async Task<ImageDto> GetImageById(Guid imageId)
    {
        if(imageId == Guid.Empty) 
            throw new ImageException($"Image {imageId} is null",StatusCodes.Status204NoContent);

        var image = await _unitOfWork.ImageRepository.GetImageById(imageId);
        if(image is null)
            throw new ImageException($"Image {imageId} not found",StatusCodes.Status404NotFound);

        var imageDto = image.ToImageDto();

        return imageDto;
    }
}