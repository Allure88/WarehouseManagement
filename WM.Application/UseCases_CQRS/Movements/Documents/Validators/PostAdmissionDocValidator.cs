using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Validators;

public class PostAdmissionDocValidator : AbstractValidator<AdmissionDocBody>
{
    public PostAdmissionDocValidator(IAdmissionDocRepository repository)
    {
        RuleFor(d => d.ResBody)
            .Must((resourceMovmn) =>
            {
                return resourceMovmn.Resource.State != State.Archived;
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
            .GreaterThan(DateTime.Now).WithMessage("Нельзя использовать предыдущую дату");
    }
}

