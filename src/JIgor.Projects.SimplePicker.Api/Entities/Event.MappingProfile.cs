using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Dtos;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class Event
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // Default map
                _ = CreateMap<Event, Event>()
                    .ReverseMap();

                _ = CreateMap<EventDto, Event>()
                    .ReverseMap();
            }
        }
    }
}
