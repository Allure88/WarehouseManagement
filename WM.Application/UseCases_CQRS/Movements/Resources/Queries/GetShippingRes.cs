using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Resources.Queries;

 public class GetShippingResBodiesListRequest : IRequest<GetShippingResBodiesListResponse> { }

public class GetShippingResBodiesListResponse(List<ShippingResEntity> ShippingRess)
{
    public List<ShippingResEntity> ShippingRess { get; } = ShippingRess;
}

public class GetShippingResBodiesListRequestHandler(IShippingResRepository repository) : IRequestHandler<GetShippingResBodiesListRequest, GetShippingResBodiesListResponse>
{
    public async Task<GetShippingResBodiesListResponse> Handle(GetShippingResBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetShippingResBodiesListResponse(entities);
    }
}