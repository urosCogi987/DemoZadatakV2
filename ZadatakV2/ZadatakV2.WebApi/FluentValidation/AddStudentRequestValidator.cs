using FluentValidation;
using ZadatakV2.Dto.Models;
using ZadatakV2.WebApi.Constants;

namespace ZadatakV2.WebApi.FluentValidation
{
    public sealed class AddStudentRequestValidator : AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()                
                .WithMessage(FluentValidationMessages.EmailFormatIncorrect);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(FluentValidationMessages.NameIsRequired);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage(FluentValidationMessages.SurnameIsRequired);

            RuleFor(x => x.Index)
                .Matches(RegularExpressions.IndexRegex)
                
                .Length(3)
                .WithMessage(FluentValidationMessages.IndexFormatIncorrect);
        }
    }
}
