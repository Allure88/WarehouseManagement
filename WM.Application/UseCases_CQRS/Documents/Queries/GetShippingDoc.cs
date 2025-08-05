using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Queries;

public class GetShippingDocBodiesListRequest : IRequest<GetShippingDocBodiesListResponse> { }

public class GetShippingDocBodiesListResponse(List<ShippingDocEntity> ShippingDocs)
{
    public List<ShippingDocEntity> ShippingDocs { get; } = ShippingDocs;
}

public class GetShippingBodiesListRequestHandler(IShippingDocRepository repository) : IRequestHandler<GetShippingDocBodiesListRequest, GetShippingDocBodiesListResponse>
{
    public async Task<GetShippingDocBodiesListResponse> Handle(GetShippingDocBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetShippingDocBodiesListResponse(entities);
    }
}