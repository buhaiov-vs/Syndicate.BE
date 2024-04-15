using FluentValidation;
using FluentValidation.Results;
using Syndicate.Services.Exceptions;

namespace Syndicate.Services.Features.Services.Models.Requests.Validators;

public class DraftServiceRequestValidator : AbstractValidator<DraftServiceRequest>
{
    public DraftServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100)
            .WithMessage(x => "{PropertyName} has invalid length. Must be between 3 and 100 characters.");
    }

    protected override void RaiseValidationException(ValidationContext<DraftServiceRequest> context, ValidationResult result)
    {
        throw new CustomValidationException(result);
    }
}
