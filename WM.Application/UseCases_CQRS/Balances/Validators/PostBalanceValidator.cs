using FluentValidation;
using WM.Application.Bodies;

namespace WM.Application.UseCases_CQRS.Balances.Validators;

public class PostBalanceValidator: AbstractValidator<BalanceBody>
{
	public PostBalanceValidator()
	{
        RuleFor(c => c.Resource)
            .NotNull().WithMessage("{ProperyName} не должно быть путым");

        RuleFor(c => c.UnitOfMeasurement)
         .NotNull().WithMessage("{ProperyName} не должно быть путым");

        RuleFor(c => c.Quantity)
            .GreaterThan(0).WithMessage("Количество должно быть положительным")
            .NotNull();
    }
}
