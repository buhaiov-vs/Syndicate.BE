using FluentValidation.Results;

namespace Syndicate.Services.Exceptions;
public class CustomValidationException(ValidationResult result) : Exception(String.Join("; ", result.Errors))
{
}
