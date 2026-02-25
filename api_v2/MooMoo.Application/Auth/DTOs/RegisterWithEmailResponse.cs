namespace MooMoo.Application.Auth.DTOs;

public class RegisterWithEmailResponse
{
    public UserDto User { get; set; } = null!;
    public string Message { get; set; } = string.Empty;
}

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
