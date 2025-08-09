using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Clients.Queries;
public class GetClientBodiesListRequest : IRequest<GetClientBodiesListResponse> { }

public class GetClientBodiesListResponse(List<ClientBody> Clients)
{
    public List<ClientBody> Clients { get; } = Clients;
}

public class GetPresetnBodiesListRequestHandler(IClientRepository repository, IMapper mapper) : IRequestHandler<GetClientBodiesListRequest, GetClientBodiesListResponse>
{
    public async Task<GetClientBodiesListResponse> Handle(GetClientBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        List<ClientBody> clients = [.. entities.Select(e => mapper.Map<ClientBody>(e))];

        return new GetClientBodiesListResponse(clients);
    }
}