using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class UpdateShippingDocValidator : AbstractValidator<ShippingDocBody>
{
    public UpdateShippingDocValidator(IShippingDocRepository repository)
    {
        RuleFor(d => d.ResBody)
            .NotNull()
            .Must((resourceMovmn) =>
            {
                return resourceMovmn.Resource.State != State.Archived;
            }).WithMessage("Архивный ресурс невозможно выбрать");

        RuleFor(c => c.Number)
             .MustAsync(async (number, token) =>
             {
                 return await repository.GetByNumber(number) is not null;
             })
            .WithMessage("Документ с номером {ComparisonValue} не существует.")
         .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
         .NotNull()
         .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
            .Must((state) => state != State.Active).WithMessage("Невозможно создать архивный документ")
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();

        RuleFor(c => c.Client)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();

        RuleFor(c => c.Date)
            .NotNull().WithMessage("{ProperyName} не должно быть путым")
            .GreaterThan(DateTime.Now).WithMessage("Нельзя использовать предыдущую дату");

    }
}

