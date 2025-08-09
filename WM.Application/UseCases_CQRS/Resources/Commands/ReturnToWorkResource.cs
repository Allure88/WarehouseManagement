using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Resources.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Commands;

public class ReturnToWorkResourceCommand(ResourceBody body) : IRequest<BaseCommandResponse>
{
    public ResourceBody Body { get; set; } = body;
}



public class ReturnToWorkResourceRequestHandler(IResourceRepository repository) : IRequestHandler<ReturnToWorkResourceCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(ReturnToWorkResourceCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await repository.GetByName(command.Body.Name);

        var validator = new ReturnToWorkResourceValidator(entity);
        var validationResult = validator.Validate(command.Body);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Ресурс не обновлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity!.State = State.Active;
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Ресурс обновлен успешно";
        }
        return response;
    }
}