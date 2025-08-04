using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Clients.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Commands;

public class DeleteClientCommand(ClientBody body) : IRequest<BaseCommandResponse>
{
    public ClientBody Body { get; set; } = body;
}

public class DeleteClientCommandHandler(IMapper mapper, IClientRepository repository) : IRequestHandler<DeleteClientCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new DeleteClientValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Клиент не удален";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ClientEntity>(command.Body);
            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Клиент удален успешно";
        }

        return response;
    }
}
