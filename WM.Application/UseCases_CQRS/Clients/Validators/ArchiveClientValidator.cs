using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class ArchiveClientValidator : AbstractValidator<ClientBody>
{
    public ClientEntity? Client { get; private set; }
    public ArchiveClientValidator(IClientRepository repository)
    {
        RuleFor(c => c.Name)
            .Custom(async (name, context) =>
            {
                Client = await repository.GetByName(name);
                if (Client is null)
                {
                    context.AddFailure(nameof(name), "Клиента с таким именем не существует");
                }
                else if (Client.State == State.Archived)
                {
                    context.AddFailure(nameof(name), "Клиент с таким именем помещен в архив ранее");
                }
            });
    }
}
