namespace MooMoo.Application.Common.DTOs;

/// <summary>
/// Standardized error response for frontend
/// </summary>
public class ErrorResponse
{
    public string Code { get; set; } = string.Empty;
    public string? Field { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

public class ApiErrorResponse
{
    public ErrorResponse Error { get; set; } = new();
    public List<ErrorResponse>? Errors { get; set; }
}
