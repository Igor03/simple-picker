using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class EventValue
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                _ = CreateMap<EventValueDto, EventValue>()
                    .ReverseMap();
            }
        }
    }
}
