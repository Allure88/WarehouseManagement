using FluentValidation;
using WM.Application.Bodies;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class PostUnitValidator : AbstractValidator<UnitBody>
{
	public PostUnitValidator()
	{
        RuleFor(c => c.UnitDescription)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull().WithMessage("{ProperyName} не должно быть путым")
            .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
            .NotNull().WithMessage("{ProperyName} не должно быть путым");

    }
}
