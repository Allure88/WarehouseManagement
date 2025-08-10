using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Mapper_Profiles;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class CreateShippingDocValidator:AbstractValidator<ShippingDocBody>
{
	public CreateShippingDocValidator(IShippingDocRepository repository)
	{
        RuleFor(d => d.ResBody)
            .NotNull()
            .Must((resourceMovmn) =>
            {
                return resourceMovmn.Resource.State.ConvertToState() != State.Archived;
            }).WithMessage("Архивный ресурс невозможно выбрать");

        RuleFor(c => c.Number)
             .MustAsync(async (number, token) =>
             {
                 return await repository.GetByNumber(number) is null;
             })
            .WithMessage("Документ с номером {ComparisonValue} не существует.")
         .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
         .NotNull()
         .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.Client)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();

        RuleFor(c => c.Date)
            .NotNull().WithMessage("{ProperyName} не должно быть путым")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Нельзя использовать предыдущую дату");

    }
}

