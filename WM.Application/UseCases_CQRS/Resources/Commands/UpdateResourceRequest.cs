using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Resources.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Commands;

public class UpdateResourceCommand(ResourceBody body) : IRequest<BaseCommandResponse>
{
    public ResourceBody Body { get; set; } = body;
}


public class UpdateResourceCommandHandler(IResourceRepository repository, IMapper mapper) : IRequestHandler<UpdateResourceCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(UpdateResourceCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateResourceValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Ресурс не добавлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ResourceEntity>(command.Body);
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Ресурс добавлен успешно";
        }

        return response;
    }
}