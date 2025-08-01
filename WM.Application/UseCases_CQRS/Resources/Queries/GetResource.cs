using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Queries;
public class GetResourceBodiesListRequest : IRequest<GetResourceBodiesListResponse> { }

public class GetResourceBodiesListResponse(List<ResourceEntity> Resources)
{
    public List<ResourceEntity> Resources { get; } = Resources;
}

public class GetPresetnBodiesListRequestHandler(IResourceRepository repository) : IRequestHandler<GetResourceBodiesListRequest, GetResourceBodiesListResponse>
{
    public async Task<GetResourceBodiesListResponse> Handle(GetResourceBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetResourceBodiesListResponse(entities);
    }
}