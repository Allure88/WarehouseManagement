using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Mapper_Profiles;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class CreateAdmissionDocValidator : AbstractValidator<AdmissionDocBody>
{
    public CreateAdmissionDocValidator(IAdmissionDocRepository repository)
    {
        RuleFor(d => d.ResBody)
            .Must((resourceMovmn) =>
            {
                return resourceMovmn.Resource.State.ConvertToState() != State.Archived;
            }).WithMessage("Архивный ресурс невозможно выбрать");


        RuleFor(d => d.Number)
              .MustAsync(async (number, token) =>
              {
                  return await repository.GetByNumber(number) is null;
              })
            .WithMessage("Документ с номером {ComparisonValue} создана ранее.")
             .NotEmpty().WithMessage("{ProperyName} не должно быть пуcтым")
             .NotNull()
             .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.Date)
            .NotNull().WithMessage("{ProperyName} не должно быть пуcтым")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Нельзя использовать предыдущую дату");
    }
}

