namespace MooMoo.Application.Exceptions;

/// <summary>
/// Exception for forbidden access
/// Maps to HTTP 403 Forbidden
/// </summary>
public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message = "Access forbidden") 
        : base(message)
    {
    }
}
