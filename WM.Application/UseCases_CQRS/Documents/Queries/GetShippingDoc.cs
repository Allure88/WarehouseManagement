using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Queries;

public class GetShippingDocBodiesListRequest : IRequest<GetShippingDocBodiesListResponse> { }

public class GetShippingDocBodiesListResponse(List<ShippingDocBody> ShippingDocs)
{
    public List<ShippingDocBody> ShippingDocs { get; } = ShippingDocs;
}

public class GetShippingBodiesListRequestHandler(IShippingDocRepository repository, IMapper mapper) : IRequestHandler<GetShippingDocBodiesListRequest, GetShippingDocBodiesListResponse>
{
    public async Task<GetShippingDocBodiesListResponse> Handle(GetShippingDocBodiesListRequest request, CancellationToken cancellationToken)
    {
        List<ShippingDocEntity> entities = await repository.GetAllWithDependents();
        entities = [.. entities.Select(e =>
        {
            e.Date = e.Date.ToLocalTime();
            return e;
        })];
        List<ShippingDocBody> docs = [.. entities.Select(mapper.Map<ShippingDocBody>)];
        return new GetShippingDocBodiesListResponse(docs);
    }
}