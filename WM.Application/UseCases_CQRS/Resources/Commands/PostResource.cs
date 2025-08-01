using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Commands;

public class PostResourceRequest(ResourceBody body) : IRequest<PostResourceResponse>
{
    public ResourceBody Body { get; set; } = body;
}

public class PostResourceResponse(ResourceEntity entity)
{
    public ResourceEntity Entity { get; set; } = entity;
}

public class PostResourceRequestHandler(IResourceRepository repository, IMapper mapper) : IRequestHandler<PostResourceRequest, PostResourceResponse>
{
    public async Task<PostResourceResponse> Handle(PostResourceRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ResourceEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostResourceResponse(addedEntity);
    }
}