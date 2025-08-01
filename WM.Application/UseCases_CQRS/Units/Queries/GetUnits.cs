using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Queries;

public class GetUnitBodiesListRequest : IRequest<GetUnitBodiesListResponse> { }

public class GetUnitBodiesListResponse(List<UnitEntity> Units)
{
    public List<UnitEntity> Units { get; } = Units;
}

public class GetPresetnBodiesListRequestHandler(IUnitsRepository repository) : IRequestHandler<GetUnitBodiesListRequest, GetUnitBodiesListResponse>
{
    public async Task<GetUnitBodiesListResponse> Handle(GetUnitBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetUnitBodiesListResponse(entities);
    }
}
