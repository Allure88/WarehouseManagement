using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Clients.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Commands;

public class PostClientCommand(ClientBody body) : IRequest<BaseCommandResponse>
{
    public ClientBody Body { get; set; } = body;
}


public class PostClientRequestHandler(IClientRepository repository, IMapper mapper) : IRequestHandler<PostClientCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostClientCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateClientValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Клиент не добавлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ClientEntity>(command.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Клиент добавлен успешно";
        }

        return response;
    }
}

