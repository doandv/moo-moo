using Microsoft.EntityFrameworkCore;
using MooMoo.Domain.Entities;

namespace MooMoo.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Profile> Profiles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
