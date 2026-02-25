using MooMoo.Domain.Common;
using MooMoo.Domain.Enums;

namespace MooMoo.Domain.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public UserRole Role { get; set; } = UserRole.PARENT;
    public UserProvider Provider { get; set; } = UserProvider.EMAIL;
    public string? ProviderId { get; set; }
    public UserStatus Status { get; set; } = UserStatus.ACTIVE;
    public bool EmailVerified { get; set; } = false;
    public DateTime? LastLoginAt { get; set; }

    // Navigation properties
    public ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
