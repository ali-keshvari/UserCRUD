using FluentValidation;
using UserCRUD.Application.Common.Constants;

namespace UserCRUD.Application.Features.User.Command.Create
{
    public class User_Create_Validator : AbstractValidator<User_Create_Command>
    {
        public User_Create_Validator()
        {
            RuleFor(u => u.FirstName)
                .NotNull().WithMessage(ValidationMessages.NotNull)
                .NotEmpty().WithMessage(ValidationMessages.NotEmpty);
            RuleFor(u => u.LastName)
                .NotNull().WithMessage(ValidationMessages.NotNull)
                .NotEmpty().WithMessage(ValidationMessages.NotEmpty);
            RuleFor(u => u.PersonalCode)
                .MinimumLength(4).WithMessage(ValidationMessages.GetMinLength("کد پرسنلی", 4))
                .MaximumLength(10).WithMessage(ValidationMessages.GetMaxLength("کد پرسنلی", 10))
                .NotNull().WithMessage(ValidationMessages.NotNull)
                .NotEmpty().WithMessage(ValidationMessages.NotEmpty);
            RuleFor(u => u.MultipleFiles)
                .NotNull().WithMessage(ValidationMessages.NotNull)
                .NotEmpty().WithMessage(ValidationMessages.NotEmpty);
            RuleFor(u => u.NationalCode)
                .NotNull().WithMessage(ValidationMessages.NotNull)
                .NotEmpty().WithMessage(ValidationMessages.NotEmpty);

        }
    }
}
