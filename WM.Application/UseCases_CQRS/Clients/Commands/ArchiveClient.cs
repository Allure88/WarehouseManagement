using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Clients.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Commands;

public class ArchiveClientCommand(ClientBody body) : IRequest<BaseCommandResponse>
{
    public ClientBody Body { get; set; } = body;
}


public class ArchiveClientRequestHandler(IClientRepository repository) : IRequestHandler<ArchiveClientCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(ArchiveClientCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new ArchiveClientValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Клиент не обновлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            ClientEntity entity = validator.Client!;
            entity.State = State.Archived;
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Клиент обновлен успешно";
        }
        return response;
    }
}

