namespace MooMoo.Application.Exceptions;

/// <summary>
/// Exception when there's a conflict (e.g., duplicate email)
/// Maps to HTTP 409 Conflict
/// </summary>
public class ConflictException : ApplicationException
{
    public string ErrorCode { get; }
    public string? Field { get; }
    
    public ConflictException(string errorCode, string? field = null) 
        : base(errorCode)
    {
        ErrorCode = errorCode;
        Field = field;
    }
}
