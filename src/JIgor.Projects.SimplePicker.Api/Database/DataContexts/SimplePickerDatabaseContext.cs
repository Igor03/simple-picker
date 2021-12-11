using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JIgor.Projects.SimplePicker.Api.Database.DataContexts
{
    public sealed class SimplePickerDatabaseContext : DbContext, ISimplePickerDatabaseContext
    {
        private readonly IConfiguration _configuration;

        public SimplePickerDatabaseContext(DbContextOptions<SimplePickerDatabaseContext> options,
            IConfiguration configuration) : base (options)
        {
            _configuration = configuration;
            Events = Set<Event>();
            EventValues = Set<EventValue>();
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventValue> EventValues { get; set; }

        public new async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return result > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            _ = modelBuilder.HasDefaultSchema("dbo")
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
