using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.UseCases_CQRS.Clients.Queries;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Queries;
public class GetResourceBodiesListRequest : IRequest<GetResourceBodiesListResponse> { }

public class GetResourceBodiesListResponse(List<ResourceBody> Resources)
{
    public List<ResourceBody> Resources { get; } = Resources;
}

public class GetPresetnBodiesListRequestHandler(IResourceRepository repository, IMapper mapper) : IRequestHandler<GetResourceBodiesListRequest, GetResourceBodiesListResponse>
{
    public async Task<GetResourceBodiesListResponse> Handle(GetResourceBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        List<ResourceBody> resources = [.. entities.Select(e => mapper.Map<ResourceBody>(e))];
        return new GetResourceBodiesListResponse(resources);
    }
}