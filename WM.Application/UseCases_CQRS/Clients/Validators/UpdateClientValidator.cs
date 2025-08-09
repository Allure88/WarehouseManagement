using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class UpdateClientValidator : AbstractValidator<ClientBody>
{
    public ClientEntity? ClientEntity { get; private set; }
    public UpdateClientValidator(ClientEntity? clientEntity)
    {
        RuleFor(c => c.Name)
            .Must((name) =>
            {
                return clientEntity is not null;
            })
            .WithMessage($"Клиента таким именем не существует. Удалите запись и создайте заново.")
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull()
            .MaximumLength(50).WithMessage("Максимальная длина 50 символов");


        RuleFor(c => c.Adress)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();
    }
}
