using FluentValidation;
using WM.Application.Bodies;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class PostClientValidator : AbstractValidator<ClientBody>
{
	public PostClientValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("{ProperyName} не должно быть путым")
			.NotNull()
			.MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("{ProperyName} не должно быть путым")
			.NotNull();

		RuleFor(c => c.Adress)
			.NotEmpty().WithMessage("{ProperyName} не должно быть путым")
			.NotNull();
			
		
		//{ComparisonValue}
    }


}
