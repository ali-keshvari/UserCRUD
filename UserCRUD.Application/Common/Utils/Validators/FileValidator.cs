using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace UserCRUD.Application.Common.Utils.Validators;

public class FileValidatorBuilder
{
    private int? Length { get; set; }
    private List<string>? Extensions { get; set; }

    public FileValidatorBuilder AddLength(int length)
    {
        Length = length;
        return this;
    }

    public FileValidatorBuilder AddExtensions(string[] extensions)
    {
        Extensions = extensions.ToList();
        return this;
    }

    public FileValidator Build()
    {
        if (Length != null && Extensions != null)
        {
            return new FileValidator((int)Length, Extensions.ToArray());
        }
        if (Length != null)
        {
            return new FileValidator((int)Length);
        }
        if (Extensions != null)
        {
            return new FileValidator(Extensions.ToArray());
        }

        return new FileValidator();
    }
}

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator() { }

    public FileValidator(int length)
    {
        var lengthInBytes = length * 1000000;

        RuleFor(x => x.Length)
            .LessThanOrEqualTo(lengthInBytes);
    }

    public FileValidator(string[] extensions)
    {
        RuleFor(x => x.ContentType)
            .Must(extensions.Contains);
    }

    public FileValidator(int length, string[] extensions)
    {
        var lengthInBytes = length * 1000000;

        RuleFor(x => x.Length)
            .LessThanOrEqualTo(lengthInBytes);

        RuleFor(x => x.ContentType)
            .Must(extensions.Contains);
    }
}
