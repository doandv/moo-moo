namespace MooMoo.Application.Exceptions;

/// <summary>
/// Exception for bad requests
/// Maps to HTTP 400 Bad Request
/// </summary>
public class BadRequestException : ApplicationException
{
    public BadRequestException(string message) : base(message)
    {
    }
}
