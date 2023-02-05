using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NicasourceChallenge.SharedKernel.Validators;

public class IFormFileValidator : AbstractValidator<IFormFile?>
{
    public IFormFileValidator()
    {
        RuleFor(x => x.Length).NotNull();
    }
}