using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Clients.Validators;
using WM.Domain.Models;

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
        var entity = await repository.GetByName(command.Body.Name); 
        var validator = new ArchiveClientValidator(entity);
        var validationResult = validator.Validate(command.Body);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Клиент не обновлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity!.State = State.Archived;
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Клиент обновлен успешно";
        }
        return response;
    }
}

