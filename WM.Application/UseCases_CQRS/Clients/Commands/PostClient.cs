using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Commands;

public class PostClientRequest(ClientBody body) : IRequest<PostClientResponse>
{
    public ClientBody Body { get; set; } = body;
}

public class PostClientResponse(ClientEntity entity)
{
    public ClientEntity Entity { get; set; } = entity;
}

public class PostClientRequestHandler(IClientRepository repository, IMapper mapper) : IRequestHandler<PostClientRequest, PostClientResponse>
{
    public async Task<PostClientResponse> Handle(PostClientRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ClientEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostClientResponse(addedEntity);
    }
}