﻿using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.Api.Database.Contracts
{
    public interface ISimplePickerDatabaseContext
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Event> Events { get; set; }

        DbSet<EventValue> EventValues { get; set; }

        DatabaseFacade Database { get; }
    }
}