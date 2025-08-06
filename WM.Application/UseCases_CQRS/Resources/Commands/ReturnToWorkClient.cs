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



public class ReturnToWorkResourceRequestHandler(IResourceRepository repository) : IRequestHandler<ArchiveResourceCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(ArchiveResourceCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new ReturnToWorkResourceValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Ресурс не обновлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            ResourceEntity entity = validator.Resource!;
            entity.State = State.Active;
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Ресурс обновлен успешно";
        }
        return response;
    }
}