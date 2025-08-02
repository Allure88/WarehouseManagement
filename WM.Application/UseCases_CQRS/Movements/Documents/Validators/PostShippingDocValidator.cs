using FluentValidation;
using WM.Application.Bodies;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Validators;

public class PostShippingDocValidator:AbstractValidator<ShippingDocBody>
{
	public PostShippingDocValidator()
	{
        RuleFor(c => c.Number)
         .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
         .NotNull()
         .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
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
