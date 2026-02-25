using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MooMoo.Domain.Entities;
using MooMoo.Domain.Enums;

namespace MooMoo.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Provider)
            .IsRequired()
            .HasConversion<string>()
            .HasDefaultValue(UserProvider.EMAIL);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(u => u.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();

        // Composite unique index
        builder.HasIndex(u => new { u.Email, u.Provider })
            .IsUnique()
            .HasDatabaseName("IX_User_Email_Provider");

        builder.HasIndex(u => new { u.Provider, u.ProviderId })
            .IsUnique()
            .HasDatabaseName("IX_User_Provider_ProviderId");

        // Relationships
        builder.HasMany(u => u.Profiles)
            .WithOne(p => p.Parent)
            .HasForeignKey(p => p.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
