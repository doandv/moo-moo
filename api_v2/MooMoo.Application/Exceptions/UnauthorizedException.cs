namespace MooMoo.Application.Exceptions;

/// <summary>
/// Exception for unauthorized access
/// Maps to HTTP 401 Unauthorized
/// </summary>
public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message = "Unauthorized access") 
        : base(message)
    {
    }
}
