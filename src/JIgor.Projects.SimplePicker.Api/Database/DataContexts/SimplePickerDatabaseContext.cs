﻿using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.Api.Database.DataContexts
{
    public sealed class SimplePickerDatabaseContext : DbContext, ISimplePickerDatabaseContext
    {
        private readonly IConfiguration _configuration;

        public SimplePickerDatabaseContext(DbContextOptions<SimplePickerDatabaseContext> options,
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Events = Set<Event>();
            EventValues = Set<EventValue>();
            Database = base.Database;
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventValue> EventValues { get; set; }

        public new DatabaseFacade Database { get; }

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