using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class ReturnToWorkClientValidator : AbstractValidator<ClientBody>
{

    public ReturnToWorkClientValidator(ClientEntity? client)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (client is null)
                {
                    context.AddFailure(nameof(name), "Клиента с таким именем не существует");
                }
                else if (client.State != State.Archived)
                {
                    context.AddFailure(nameof(name), "Клиент с таким именем не находится в архиве");
                }
            });
    }
}
