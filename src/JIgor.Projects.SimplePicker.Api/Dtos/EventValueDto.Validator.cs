using FluentValidation;

namespace JIgor.Projects.SimplePicker.Api.Dtos
{
    public partial class EventValueDto
    {
        public class Validator : AbstractValidator<EventValueDto>
        {
            public Validator()
            {
                _ = RuleFor(p => p.Value)
                    .NotEmpty().WithMessage("You have to provide some value to {PropertyName}");
            }
        }
    }
}
