namespace WM.Application.UseCases_CQRS.States.Validators;
using FluentValidation;
using WM.Application.Bodies;

public class PostStateValidator : AbstractValidator<StateBody>
{
	public PostStateValidator()
	{
        RuleFor(c => c.State)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull()
            .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");
    }
}
