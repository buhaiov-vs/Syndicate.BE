using FluentValidation;
using FluentValidation.Results;
using Syndicate.Services.Exceptions;
using Syndicate.Services.Features.Services.Models.Requests;

namespace Syndicate.Services.Features.Services.Validators;

public class CreateFolderServiceRequestValidator : AbstractValidator<CreateServicesFolderRequest>
{
    public CreateFolderServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage(x => "{PropertyName} has invalid length. Must be between 3 and 20 characters.");
    }

    protected override void RaiseValidationException(ValidationContext<CreateServicesFolderRequest> context, ValidationResult result)
    {
        throw new CustomValidationException(result);
    }
}
