using MooMoo.Domain.Common;

namespace MooMoo.Domain.Entities;

public class Profile : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public int Grass { get; set; } = 0; // Cỏ để cho bò ăn (nhận từ task)
    public int Gold { get; set; } = 0; // Vàng để đổi quà (bán bò)
    public int ParentId { get; set; }
    public string? PinCode { get; set; }

    // Navigation properties
    public User Parent { get; set; } = null!;
}
