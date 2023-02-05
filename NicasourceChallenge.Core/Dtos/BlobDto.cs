using FluentValidation;
using Microsoft.AspNetCore.Http;
using NicasourceChallenge.SharedKernel.Validators;

namespace NicasourceChallenge.Core.Dtos;

public class BlobDto
{
    public IFormFile? File { get; set; }
    public string? Description { get; set; }
}

public class BlobDtoValidator : AbstractValidator<BlobDto>
{
    public BlobDtoValidator()
    {
        RuleFor(x => x.File).SetValidator(new IFormFileValidator());
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}