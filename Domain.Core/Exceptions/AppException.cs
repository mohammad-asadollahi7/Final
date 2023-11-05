
namespace Domain.Core.Exceptions;

public class AppException : Exception
{
    private readonly string _message;
    public AppException(string message, int statusCode) : base(message)
    {
        _message = message;
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}
