using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Dto.Storage;
using Application.IServices;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class StorageService : IStorageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _bucketName;
    private readonly AmazonS3Client _s3Client;

    public StorageService(IConfiguration configuration,IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _bucketName = configuration["AWS_STORAGE_BUCKET_NAME"]!;

        var awsAccessKey = configuration["AWS_ACCESS_KEY_ID"]!;
        var awsSecretKey = configuration["AWS_SECRET_ACCESS_KEY"]!;

        var awsConfig = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUNorth1,
        };

        var basicAwsCredentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);

        _s3Client = new AmazonS3Client(basicAwsCredentials, awsConfig);
    }
    
    //TODO:DeleteImagesByCategoryIdAsync
    public async Task RemoveCategoryByCategoryId(Guid categoryId)
    {
        var categoryWithImages = await _unitOfWork.ImageRepository.GetImagesByCategoryId(categoryId);

        var deleteObjRequest = new DeleteObjectsRequest
        {
            BucketName = _bucketName,
            Objects = categoryWithImages.Select(x => new KeyVersion{Key = x}).ToList()
        };

        await _unitOfWork.CategoryRepository.DeleteCategoryById(categoryId);

        await _s3Client.DeleteObjectsAsync(deleteObjRequest);
    }
    
    //TODO:Change to work with many request
    public async Task<PreSignedUrlResponseDto> AddPreSignedUrlAsync(PreSignedUrlRequestDto preSignedUrlRequestDto)
    {
        var path = $"{preSignedUrlRequestDto.FolderName}/{Guid.NewGuid()}{preSignedUrlRequestDto.Extension}";

        var preSignedUrlRequest = new GetPreSignedUrlRequest
        {
            Verb = HttpVerb.PUT,
            BucketName = _bucketName,
            Key = path,
            Expires = DateTime.UtcNow.AddSeconds(30),
        };

        var preSignedUrl = await _s3Client.GetPreSignedURLAsync(preSignedUrlRequest);

        var result = new PreSignedUrlResponseDto
        {
            PreSignedUrl = preSignedUrl,
            ImagePath = path
        };

        return result;
    }
}