using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.States.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.States.Commands;


public class PostStateRequest(StateBody body) : IRequest<BaseCommandResponse>
{
    public StateBody Body { get; set; } = body;
}


public class PostStateRequestHandler(IStateRepository repository, IMapper mapper) : IRequestHandler<PostStateRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostStateRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostStateValidator();
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Состояние не добавлено";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<StateEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Состояние добавлено успешно";
        }

        return response;
    }
}