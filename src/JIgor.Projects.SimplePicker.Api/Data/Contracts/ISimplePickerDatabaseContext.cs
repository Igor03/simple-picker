using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace JIgor.Projects.SimplePicker.Api.Data.Contracts
{
    public interface ISimplePickerDatabaseContext
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Event> Events { get; set; }

        DbSet<EventValue> EventValues { get; set; }
    }
}
