using FluentValidation;
using System;
using System.Linq;

namespace JIgor.Projects.SimplePicker.Api.Dtos
{
    public partial class EventDto
    {
        public class Validator : AbstractValidator<EventDto>
        {
            public Validator()
            {
                _ = RuleFor(p => p.Title)
                    .NotEmpty().WithMessage("You have to provide a value to {PropertyName}")
                    .MaximumLength(50).WithMessage("The {PropertyName} cannot be over 50 characters");

                _ = RuleFor(p => p.Description)
                    .MaximumLength(255).WithMessage("The {PropertyName} cannot be over 255 characters");

                _ = RuleFor(p => p.StartDate)
                    .NotEqual(default(DateTime)).WithMessage("You have to provide a valid date for {PropertyName}")
                    .NotNull().WithMessage("You have to provide a valid date for {PropertyName}")
                    .Must(p => p.Date >= DateTime.Today.Date).WithMessage("You have to provide a valid date for {PropertyName}");

                _ = RuleFor(p => p.DueDate)
                    .NotEqual(default(DateTime)).WithMessage("You have to provide a valid value for {PropertyName}")
                    .GreaterThan(p => p.StartDate).WithMessage("Initial date have to be greater than Due date");

                _ = RuleFor(p => p.EventValues)
                    .NotNull().WithMessage("You have to provide, at least, a value to create an event.")
                    .Must(p => p!.Any()).WithMessage("You have to provide, at least, a value to create an event.");
            }
        }
    }
}