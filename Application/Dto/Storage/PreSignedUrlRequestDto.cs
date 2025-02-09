namespace Application.Dto.Storage;

public class PreSignedUrlRequestDto
{
    public required string FolderName { get; set; }
    public required string Extension { get; set; }
}