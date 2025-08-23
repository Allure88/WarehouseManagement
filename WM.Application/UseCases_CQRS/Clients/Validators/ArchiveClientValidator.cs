using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class ArchiveClientValidator : AbstractValidator<ClientBody>
{
    public ArchiveClientValidator(ClientEntity? clientEntity)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (clientEntity is null)
                {
                    context.AddFailure(nameof(name), "Клиента с таким именем не существует");
                }
                else if (clientEntity.State == State.Archived)
                {
                    context.AddFailure(nameof(name), "Клиент с таким именем помещен в архив ранее");
                }
            });
    }
}
