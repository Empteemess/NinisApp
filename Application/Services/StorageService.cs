using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Application.IServices;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class StorageService : IStorageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _bucketName;
    private readonly string? _baseUrl;
    private readonly AmazonS3Client _s3Client;

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

        _s3Client = new AmazonS3Client(basicAwsCredentials, awsConfig);
    }


    public async Task<string> GetPreSignedUrlAsync(string folderName, string extension)
    {
        var preSignedUrlRequest = new GetPreSignedUrlRequest
        {
            Verb = HttpVerb.PUT,
            BucketName = _bucketName,
            Key = folderName,
            Expires = DateTime.UtcNow.AddSeconds(30),
        };

        var preSignedUrl = await _s3Client.GetPreSignedURLAsync(preSignedUrlRequest);
        return preSignedUrl;
    }

}