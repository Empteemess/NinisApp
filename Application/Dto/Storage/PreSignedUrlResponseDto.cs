namespace Application.Dto.Storage;

public class PreSignedUrlResponseDto
{
    public required string PreSignedUrl { get; set; }
    public required string ImagePath { get; set; }
}