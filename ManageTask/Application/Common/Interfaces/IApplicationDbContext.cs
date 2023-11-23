using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<TheTask> Tasks { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
