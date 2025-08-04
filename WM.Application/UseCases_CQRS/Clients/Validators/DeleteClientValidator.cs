using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class DeleteClientValidator : AbstractValidator<ClientBody>
{
    public DeleteClientValidator(IClientRepository repository)
    {
		RuleFor(c => c.Name)
			.Custom(async (name, context) =>
			{
				var client = await repository.GetByNameWithDependents(name);
				if (client is null)
				{
					context.AddFailure(nameof(name), "Клиента с таким именем не существует");
				}
				else if (client.ShippingDocuments.Count != 0)
				{
					context.AddFailure(nameof(name), "Клиент с таким именем используется в документах, и не может быть удален");
				}
			});
        RuleFor(c => c.Adress)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();
    }
}
