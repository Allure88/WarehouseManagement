using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.States.Commands;


public class PostStateRequest(StateBody body) : IRequest<PostStateResponse>
{
    public StateBody Body { get; set; } = body;
}

public class PostStateResponse(StateEntity entity)
{
    public StateEntity Entity { get; set; } = entity;
}

public class PostStateRequestHandler(IStateRepository repository, IMapper mapper) : IRequestHandler<PostStateRequest, PostStateResponse>
{
    public async Task<PostStateResponse> Handle(PostStateRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<StateEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostStateResponse(addedEntity);
    }
}