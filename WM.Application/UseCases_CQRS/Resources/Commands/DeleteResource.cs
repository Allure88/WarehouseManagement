using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Resources.Validators;
using WM.Domain.Entities;

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
        var validator = new DeleteResourceValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Ресурс не удален";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ResourceEntity>(command.Body);
            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Ресурс удален успешно";
        }

        return response;
    }
}
