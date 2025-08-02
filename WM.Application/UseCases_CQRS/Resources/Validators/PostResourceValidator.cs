using FluentValidation;
using WM.Application.Bodies;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class PostResourceValidator:AbstractValidator<ResourceBody>
{
	public PostResourceValidator()
	{
        RuleFor(c => c.Name)
           .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
           .NotNull()
           .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
                .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
                .NotNull();
    }


   
}
