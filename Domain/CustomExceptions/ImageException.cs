namespace Domain.CustomExceptions;

public class ImageException : Exception
{
    public readonly int StatusCode;

    public ImageException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public ImageException(string message) : base(message)
    {
    }
}