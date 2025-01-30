using System.Collections.Concurrent;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Application.Dto;
using Application.IServices;
using Application.Mappers;
using Application.Mappers.Image;
using Domain.CustomExceptions;
using Domain.Enity;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class StorageService : IStorageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _bucketName;
    private readonly TransferUtility _transferUtility;
    private readonly string? _baseUrl;

    public StorageService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _bucketName = configuration["AWS_STORAGE_BUCKET_NAME"]!;

        var awsAccessKey = configuration["AWS_ACCESS_KEY_ID"]!;
        var awsSecretKey = configuration["AWS_SECRET_ACCESS_KEY"]!;

        _baseUrl = configuration["AWS_BASE_URL"];

        var awsConfig = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUNorth1,
        };

        var basicAwsCredentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);

        var s3Client = new AmazonS3Client(basicAwsCredentials, awsConfig);

        _transferUtility = new TransferUtility(s3Client);
    }
    
    public async Task RemoveCategoryByNameAsync(string categoryName)
    {
        var category = await CheckAndGetImageByCategoryName(categoryName);

        foreach (var imageLink in category.ImageLinks)
        {
            await _transferUtility.S3Client.DeleteObjectAsync(_bucketName, imageLink);
        }
        
        _unitOfWork.IImageRepository.DeleteImage(category);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Image> CheckAndGetImageByCategoryName(string categoryName)
    {
        var image = await _unitOfWork.IImageRepository.GetByCategoryByName(categoryName);
        if(image is null)
            throw new ImageException($"{categoryName} is not found",StatusCodes.Status404NotFound);
        
        if(image.ImageLinks is null)
            throw new ImageException($"Links is not found",StatusCodes.Status404NotFound);
        
        return image;
    }
    
    public async Task UploadFileAsync(GetImageDto getImageDto)
    {
        await CategoryCheck(getImageDto.CategoryName);
        
        ConcurrentBag<ImagePath> imagePaths = [];

        await Parallel.ForEachAsync(getImageDto.Images, async (image, _) =>
        {
            var imageCheck = IsImageFile(image);
            if (!imageCheck)
                throw new ImageException("Invalid image format",StatusCodes.Status409Conflict);

            //Memory Streams
            var memoryStream = await ConvertImageToMemoryStream(image);

            //Convert To WebP
            var webpImage = await ConvertImageToWebp(memoryStream);

            var guid = Guid.NewGuid();
            var filePath = $"{getImageDto.CategoryName}/{guid}{Path.GetExtension(image.FileName)}";

            var result = new ImagePath { Image = webpImage, Path = filePath };
            imagePaths.Add(result);
        });

        try
        {
            await UploadFileAsync(imagePaths);
            await _unitOfWork.IImageRepository.AddImageAsync(imagePaths.ToImage(getImageDto.CategoryName));
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            foreach (var imagePath in imagePaths)
            {
                await _transferUtility.S3Client.DeleteObjectAsync(_bucketName, imagePath.Path);
            }
        }
    }

    private async Task UploadFileAsync(IEnumerable<ImagePath> imagePath)
    {
        var parallelOptions = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 10
        };

        await Parallel.ForEachAsync(imagePath, parallelOptions, async (file, _) =>
        {
            file.Image.Position = 0;
            var transferUtilityUploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = _bucketName,
                CannedACL = S3CannedACL.NoACL,
                Key = file.Path,
                InputStream = file.Image
            };

            await _transferUtility.UploadAsync(transferUtilityUploadRequest);
        });
    }

    private async Task CategoryCheck(string categoryName)
    {
        var category = await _unitOfWork.IImageRepository.GetByCategoryByName(categoryName);
        if (category is not null)
            throw new ImageException("Category already exists",StatusCodes.Status403Forbidden);
    }

    private bool IsImageFile(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".svg" };
        if (imageExtensions.Contains(fileExtension) is false)
            return false;

        return true;
    }

    private async Task<MemoryStream> ConvertImageToWebp(MemoryStream memoryStream)
    {
        using var image = await SixLabors.ImageSharp.Image.LoadAsync(memoryStream);
        memoryStream.SetLength(0);
        var webpEncoder = new SixLabors.ImageSharp.Formats.Webp.WebpEncoder
        {
            Quality = 69
        };

        await image.SaveAsync(memoryStream, webpEncoder);
        memoryStream.Position = 0;

        return memoryStream;
    }

    private async Task<MemoryStream> ConvertImageToMemoryStream(IFormFile image)
    {
        var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        return memoryStream;
    }
}