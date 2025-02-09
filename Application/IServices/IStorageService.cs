namespace Application.IServices;

public interface IStorageService
{
    Task<string> GetPreSignedUrlAsync(string folderName, string extension);
}