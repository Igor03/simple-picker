using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JIgor.Projects.SimplePicker.Api.Commons.HealthCheckers
{
    public class DatabaseHealthChecker : IHealthCheck
    {
        private readonly ISimplePickerDatabaseContext _databaseContext;

        public DatabaseHealthChecker(ISimplePickerDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var canConnect = await this._databaseContext.Database.CanConnectAsync(cancellationToken)
                .ConfigureAwait(default);

            if (canConnect) return await Task.FromResult(HealthCheckResult.Healthy($"Successfully connected with the {_databaseContext.Database.ProviderName}"));
            
            return await Task.FromResult(HealthCheckResult.Unhealthy("Cannot connect to the database"));
        }
    }
}