using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class PostClientValidator : AbstractValidator<ClientBody>
{
	public PostClientValidator(IClientRepository repository)
	{
		RuleFor(c => c.Name)
			.MustAsync( async (name, token) =>
			{
				return await repository.GetByName(name) is null;
			})
			.WithMessage("Клиент с именем {ComparisonValue} создан ранее.")
			.NotEmpty().WithMessage("{ProperyName} не должно быть путым")
			.NotNull()
			.MaximumLength(50).WithMessage("Максимальная длина 50 символов");


		RuleFor(c => c.Adress)
			.NotEmpty().WithMessage("{ProperyName} не должно быть путым")
			.NotNull();
    }


}
