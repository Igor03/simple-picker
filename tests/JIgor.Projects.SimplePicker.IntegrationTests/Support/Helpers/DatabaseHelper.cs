using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers
{
    public class DatabaseHelper
    {
        private ISimplePickerDatabaseContext _databaseContext;

        public DatabaseHelper(ISimplePickerDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<object> GetResource(object resource)
        {
            var x = await _databaseContext.Events.ToListAsync()
                .ConfigureAwait(default);

            return x;
        }
    }
}