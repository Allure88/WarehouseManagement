using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Resources.Validators;

namespace WM.Application.UseCases_CQRS.Resources.Commands;

public class DeleteResourceCommand(ResourceBody body) : IRequest<BaseCommandResponse>
{
    public ResourceBody Body { get; set; } = body;
}

public class DeleteResourceCommandHandler(IMapper mapper, IResourceRepository repository) : IRequestHandler<DeleteResourceCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(DeleteResourceCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await repository.GetByNameWithDependents(command.Body.Name);
        var validator = new DeleteResourceValidator(entity);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Ресурс не удален";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity = mapper.Map(command.Body, entity!);
            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Ресурс удален успешно";
        }

        return response;

    }
}
