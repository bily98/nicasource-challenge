﻿using FluentValidation;
using NicasourceChallenge.SharedKernel.Entities;

namespace NicasourceChallenge.Core.Entities;

public class Document : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public float Size { get; set; }
    public string? Format { get; set; }
}

public class DocumentValidator : AbstractValidator<Document>
{
    public DocumentValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}