using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NicasourceChallenge.SharedKernel.Validators;

public class FileValidator : AbstractValidator<IFormFile?>
{
    public FileValidator()
    {
        RuleFor(x => x.Length).NotNull();
    }
}