using FluentValidation;
using Syndicate.Services.Features.Services.Models.Requests;

namespace Syndicate.Services.Features.Services.Validators;

public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequest>
{
    public UpdateServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Duration)  // 5m to 11h 55m
            .LessThan(715)
            .GreaterThan(5);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .LessThan(100_000);

        RuleFor(x => x.Tags)
            .Must(x => x.Count <= 10);
    }
}
