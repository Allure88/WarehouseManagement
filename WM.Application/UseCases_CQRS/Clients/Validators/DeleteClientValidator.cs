using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class DeleteClientValidator : AbstractValidator<ClientBody>
{
    public DeleteClientValidator(ClientEntity? ClientEntity)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (ClientEntity is null)
                {
                    context.AddFailure(nameof(name), "Клиента с таким именем не существует");
                }
                else if (ClientEntity.ShippingDocuments.Count != 0)
                {
                    context.AddFailure(nameof(name), "Клиент с таким именем используется в документах, и не может быть удален");
                }
            });
        RuleFor(c => c.Adress)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();
    }
}
