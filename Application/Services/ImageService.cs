using Application.Dto.Image;
using Domain.IRepositories;

namespace Application.Services;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;

    public ImageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ImageDto> GetImageById(Guid iamgeId)
    {
        //აქ თუ რამე custom Exception გექნება ისევ
        if(iamgeId == Guid.Empty) 
            throw new ArgumentNullException(nameof(iamgeId));

        var image = await _unitOfWork.ImageRepository.GetImageById(iamgeId);

        if(image == null)
            throw new ArgumentNullException(nameof(image), "Image Not Found");

        var imageDto = new ImageDto
        {
            Id = image.Id,
            ImageLink = image.ImageLink
        };

        return imageDto;
    }
}