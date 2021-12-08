using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class Event
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                _ = CreateMap<EventDto, Event>()
                    .ReverseMap();

                _ = CreateMap<CreateEventRequestDto, Event>();
            }
        }
    }
}
