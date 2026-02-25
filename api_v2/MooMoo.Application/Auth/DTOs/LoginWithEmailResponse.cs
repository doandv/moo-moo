namespace MooMoo.Application.Auth.DTOs;

public class LoginWithEmailResponse
{
    public UserDto User { get; set; } = new();
    public string AccessToken { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
