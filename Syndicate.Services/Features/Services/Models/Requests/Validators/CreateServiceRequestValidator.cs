using FluentValidation;

namespace Syndicate.Services.Features.Services.Models.Requests.Validators;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);
        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(3, 500);
        RuleFor(x => x.Tags)
            .Must(x => x.Count <= 10);
    }
}
