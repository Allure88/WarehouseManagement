using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Clients.Queries;
public class GetClientBodiesListRequest : IRequest<GetClientBodiesListResponse> { }

public class GetClientBodiesListResponse(List<ClientEntity> Clients)
{
    public List<ClientEntity> Clients { get; } = Clients;
}

public class GetPresetnBodiesListRequestHandler(IClientRepository repository) : IRequestHandler<GetClientBodiesListRequest, GetClientBodiesListResponse>
{
    public async Task<GetClientBodiesListResponse> Handle(GetClientBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetClientBodiesListResponse(entities);
    }
}