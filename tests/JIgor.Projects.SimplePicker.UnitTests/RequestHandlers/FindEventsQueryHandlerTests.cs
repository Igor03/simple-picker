using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using static JIgor.Projects.SimplePicker.UnitTests.RequestHandlers.FindEventsQueryHandlerTests.DataSource;

namespace JIgor.Projects.SimplePicker.UnitTests.RequestHandlers
{
    [TestClass]
    public partial class FindEventsQueryHandlerTests
    {
        [TestMethod]
        public async Task FindEventsQueryHandlerShouldReturnExpectedResult()
        {
        }

        private static FindEventsQueryHandler CreateEventsQueryHandler(
            IEventRepository eventRepository = null,
            IMapper mapper = null)
        {
            eventRepository ??= Substitute.For<IEventRepository>();
            mapper ??= Substitute.For<IMapper>();

            return new FindEventsQueryHandler(eventRepository, mapper);
        }
    }
}
