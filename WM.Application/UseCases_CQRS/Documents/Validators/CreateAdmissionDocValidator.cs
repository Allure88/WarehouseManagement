using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Mapper_Profiles;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class CreateAdmissionDocValidator : AbstractValidator<AdmissionDocBody>
{
    public CreateAdmissionDocValidator(IAdmissionDocRepository repository)
    {

        RuleFor(d => d.Number)
             .NotEmpty().WithMessage("Поле номер не должно быть пуcтым")
             .NotNull()
             .MaximumLength(50).WithMessage("Максимальная длина поля номер 50 символов");

        RuleFor(c => c.Date)
            .NotNull().WithMessage("Поле дата не должно быть пуcтым");

    }
}

