namespace Domain.CustomExceptions;

public class CategoryException : Exception
{
    public readonly int StatusCode;

    public CategoryException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public CategoryException(string message) : base(message)
    {
    }
}