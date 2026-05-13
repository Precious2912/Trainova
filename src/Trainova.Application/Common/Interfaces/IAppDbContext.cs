using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Entities;

namespace Trainova.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
