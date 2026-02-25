namespace MooMoo.Application.Exceptions;

/// <summary>
/// Exception when entity is not found
/// Maps to HTTP 404 Not Found
/// </summary>
public class NotFoundException : ApplicationException
{
    public string ErrorCode { get; }
    public string? Field { get; }
    
    public NotFoundException(string errorCode, string? field = null) 
        : base(errorCode)
    {
        ErrorCode = errorCode;
        Field = field;
    }
}
