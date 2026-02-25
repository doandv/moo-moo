using MooMoo.Domain.Entities;

namespace MooMoo.Application.Common.Interfaces;

public interface IJwtService
{
    /// <summary>
    /// Generate access token for authenticated user
    /// </summary>
    string GenerateAccessToken(User user);

    /// <summary>
    /// Generate refresh token
    /// </summary>
    string GenerateRefreshToken();

    /// <summary>
    /// Validate token and extract user ID
    /// </summary>
    int? ValidateToken(string token);
}
