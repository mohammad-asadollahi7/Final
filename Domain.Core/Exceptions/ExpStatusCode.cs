
namespace Domain.Core.Exceptions;

public static class ExpStatusCode
{
    public static int Unauthorized { get; } = 401;
    public static int Conflict { get; } = 409;

    public static int NotFound { get; } = 404;

    public static int InternalServerError { get; } = 500;
    public static int BadRequest { get; } = 400;
}
